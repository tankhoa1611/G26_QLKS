namespace WebApplicationQLKS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    public class RoomModel
    {
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<ApplicationContext> ApplicationContexts { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
