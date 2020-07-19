using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationQLKS.Models
{
    public class OrderModel
    {
        public IEnumerable<Guest> Guests { get; set; }
        public Guest Guest { get; set; }
        public ApplicationContext ApplicationContext { get; set; }
        public IEnumerable<ApplicationContext> ApplicationContexts { get; set; }
        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}