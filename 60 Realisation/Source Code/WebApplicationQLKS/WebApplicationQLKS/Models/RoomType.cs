using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationQLKS.Models
{    
    public class RoomType
    {
        [Key]
        [DisplayName("Mã loại phòng")]
        public int TypeID { get; set; }
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        [DisplayName("Loại phòng")]
        public string TypeName { get; set; }
        
        [DisplayName("Giá phòng")]
        //[Range(1,100000000)]
        [Required]
        public double Price { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}