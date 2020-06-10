using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [StringLength(255, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên!", MinimumLength = 6)]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Nhập lại mật khẩu không được để trống!")]
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và Xác nhận mật khẩu không khớp nhau!!!")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
