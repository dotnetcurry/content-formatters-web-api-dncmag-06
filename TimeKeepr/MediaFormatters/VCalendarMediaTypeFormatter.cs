using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TimeKeepr.Models;

namespace TimeKeepr.MediaFormatters
{
    public class VCalendarMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public VCalendarMediaTypeFormatter()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/calendar"));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            headers.Add("content-disposition", (new ContentDispositionHeaderValue("attachment") { FileName = "myfile.ics" }).ToString());
            base.SetDefaultContentHeaders(type, headers, mediaType);
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(TimeCard);
        }

        public override void WriteToStream(Type type, object value, Stream stream, System.Net.Http.HttpContent content)
        {
            using (var writer = new StreamWriter(stream))
            {
                var timeCard = value as TimeCard;
                if (timeCard == null)
                {
                    throw new InvalidOperationException("Cannot serialize type");
                }
                WriteVCalendar(timeCard, writer);
            }
            stream.Close();
        }

        private void WriteVCalendar(TimeCard contactModel, StreamWriter writer)
        {
            var buffer = new StringBuilder();
            buffer.AppendLine("BEGIN:VCALENDAR");
            buffer.AppendLine("VERSION:2.0");
            buffer.AppendLine("PRODID:-//DNC/DEMOCAL//NONSGML v1.0//EN");
            buffer.AppendLine("BEGIN:VEVENT");
            buffer.AppendLine("UID:uid1@example.com");
            buffer.AppendFormat("STATUS:{0}\r\n", contactModel.Status);
            buffer.AppendFormat("DTSTART:{0}Z\r\n", (contactModel.StartDate.ToFileTimeUtc().ToString()));
            buffer.AppendFormat("DTEND:{0}Z\r\n", (contactModel.EndDate.ToFileTime().ToString()));
            buffer.AppendFormat("SUMMARY:{0}\r\n", contactModel.Summary);
            buffer.AppendFormat("DESCRIPTION:{0}\r\n", contactModel.Description);
            buffer.AppendLine("END:VEVENT");
            buffer.AppendLine("END:VCALENDAR");
            writer.Write(buffer);
        }
    }
}