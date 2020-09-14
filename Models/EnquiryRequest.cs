using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTokenAuthentication.Models
{
    public class EnquiryRequest
    {
        public int SelfAge { get; set; }
        public int SpouseAge { get; set; }
        public int Child1Age { get; set; }
        public int Child2Age { get; set; }
        public int Child3Age { get; set; }
        public int FatherAge { get; set; }
        public int MotherAge { get; set; }
        public DateTime TripStartDate { get; set; }
        public DateTime TripEndDate { get; set; }
        public int TravelFrequency { get; set; }
        public Boolean MedicalCondition { get; set; }
    }

}