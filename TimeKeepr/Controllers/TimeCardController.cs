using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TimeKeepr.Models
{
    public class TimeCardController : ApiController
    {
        private TimeKeeprContext db = new TimeKeeprContext();

        // GET api/TimeCard
        public IEnumerable<TimeCard> GetTimeCards()
        {
            return db.TimeCards.AsEnumerable();
        }

        // GET api/TimeCard/5
        public TimeCard GetTimeCard(int id)
        {
            TimeCard timecard = db.TimeCards.Find(id);
            if (timecard == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return timecard;
        }

        // PUT api/TimeCard/5
        public HttpResponseMessage PutTimeCard(int id, TimeCard timecard)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != timecard.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(timecard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/TimeCard
        public HttpResponseMessage PostTimeCard([FromBody]TimeCard timecard)
        {
            if (ModelState.IsValid)
            {
                db.TimeCards.Add(timecard);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, timecard);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = timecard.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/TimeCard/5
        public HttpResponseMessage DeleteTimeCard(int id)
        {
            TimeCard timecard = db.TimeCards.Find(id);
            if (timecard == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.TimeCards.Remove(timecard);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, timecard);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}