using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationQLKS.Models
{
    public class Payment
    {
        [Key]
        [DisplayName("Mã hoá đơn")]
        public int PaymentId { get; set; }
        [DisplayName("Mã khách hàng")]        
        public string CustomerId { get; set; }
        [DisplayName("Thành tiền")]        
        public double TotalPay { get; set; }
        [DisplayName("Trị giá")]
        public double Factor { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}