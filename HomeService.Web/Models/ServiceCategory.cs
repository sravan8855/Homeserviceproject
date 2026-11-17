using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeService.Web.Models
{
    public class ServiceCategory
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryImage { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}