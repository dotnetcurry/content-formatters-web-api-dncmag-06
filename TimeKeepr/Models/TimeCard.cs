using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeepr.Models
{
    public class TimeCard
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}