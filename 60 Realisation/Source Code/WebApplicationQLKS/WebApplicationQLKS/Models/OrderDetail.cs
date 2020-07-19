using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationQLKS.Models
{
    public class OrderDetail
    {
        [Key]
        [DisplayName("Mã chi tiết đặt phòng")]
        public int OrderDetailId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("ApplicationContext")]
        public string UserId { get; set; }
        public virtual ApplicationContext ApplicationContext { get; set; }
        [ForeignKey("Guest")]
        public int? GuestId { get; set; }
        public virtual Guest Guest { get; set; }        
    }
}