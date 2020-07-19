namespace WebApplicationQLKS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [Key]
        [DisplayName("Mã phòng")]
        public int RoomId { get; set; }    
        //[Range(101,999,ErrorMessage ="RoomNumber must be from 101 to 999")]
        [DisplayName("Số Phòng")]
        [Required]
        public int RoomName { get; set; }
        [ForeignKey("RoomType")]
        [Required]
        public int TypeID { get; set; }
        [ForeignKey("RoomStatus")]
        [Required]
        public int StatusID { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual RoomStatus RoomStatus { get; set; }
    }
}
