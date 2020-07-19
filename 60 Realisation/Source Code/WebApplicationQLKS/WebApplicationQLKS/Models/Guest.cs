using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationQLKS.Models
{
    public class Guest
    {
        [Key]
        [DisplayName("Mã khách hàng")]
        public int GuestId { get; set; }
        [Required]
        [DisplayName("Địa chỉ")]
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string Address { get; set; }
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        [DisplayName("Họ tên")]
        [Required]
        public string FullName { get; set; }
        [DisplayName("CMND")]
        [Required]
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string IdentityNumber { get; set; }
        [StringLength(20, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        [Required]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        [DisplayName("Quốc tịch")]
        [Required]
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string Nation { get; set; }

    }
}