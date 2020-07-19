namespace WebApplicationQLKS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [Key]
        [DisplayName("Mã đặt phòng")]
        public int OrderId { get; set; }
        public bool Confirm { get; set; }
        public bool Pay { get; set; }
        [Range(1,3,ErrorMessage ="Số khách phải từ 1 -> 3")]
        [DisplayName("Số lượng khách")]
        public int QuantityGuest { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [DisplayName("Check In")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckIn { get; set; }
        [DisplayName("Check Out")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckOut { get; set; }
        public virtual Room Room { get; set; }
        //public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
