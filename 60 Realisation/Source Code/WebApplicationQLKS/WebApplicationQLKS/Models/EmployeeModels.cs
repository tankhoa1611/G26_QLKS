using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationQLKS.Models
{
    public class EmployeeModels
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [MaxLength(50)]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        [DisplayName("Họ tên")]
        [Required]
        public string FullName { get; set; }
        [DisplayName("Tên đăng nhập")]
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 2)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 2)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không khớp, vui lòng thử lại")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Địa chỉ")]        
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string Address { get; set; }
        [DisplayName("CMND")]
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string IdentityNumber { get; set; }
        [DisplayName("Số điện thoại")]
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string PhoneNumber { get; set; }
        [DisplayName("Chức vụ")]
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string Role { get; set; }
    }
}