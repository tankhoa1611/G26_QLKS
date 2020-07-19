using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationQLKS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }        
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }        

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string FullName { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không khớp, vui lòng thử lại")]
        public string ConfirmPassword { get; set; }        
        [DisplayName("Địa chỉ")]
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string Address { get; set; }
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        [DisplayName("Họ tên")]        
        public string FullName { get; set; }
        [DisplayName("CMND")]        
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string IdentityNumber { get; set; }
        [StringLength(20, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]        
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        [DisplayName("Quốc tịch")]        
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string Nation { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
