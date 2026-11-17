using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeService.Web.Models
{
    public class Provider
    {
        public int ProviderId { get; set; }

        public string FullName { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int ServiceId { get; set; }

        public string Experience { get; set; }

        public string Location { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsApproved { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; }
    }
}