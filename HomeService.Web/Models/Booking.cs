using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeService.Web.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string CustomerName { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public DateTime BookingDate { get; set; }

        public string BookingTime { get; set; }

        public int ProviderId { get; set; }

        public string BookingStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public bool IsReviewed { get; set; }

        public decimal ServicePrice { get; set; }


        public decimal PlatformCommission { get; set; }

        public decimal ProviderAmount { get; set; }
    }
}