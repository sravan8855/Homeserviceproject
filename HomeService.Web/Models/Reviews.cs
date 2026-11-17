using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeService.Web.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int BookingId { get; set; }

        public int ProviderId { get; set; }

        public int CustomerId { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}