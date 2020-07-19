using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationQLKS.Models
{
    public class RoomStatus
    {
        [Key]
        [DisplayName("Mã trạng thái")]
        public int StatusID { get; set; }
        [MaxLength(50)]
        [DisplayName("Trạng thái")]
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string StatusName { get; set; }
        [Required]
        [DisplayName("Màu trạng thái")]
        public string StatusColor { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}