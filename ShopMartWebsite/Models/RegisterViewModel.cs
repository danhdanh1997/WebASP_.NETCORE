using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Không được để trống Họ và tên!")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Không được để trống Email!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không được để trống Mật khẩu!")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên!", MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Không được để trống Nhập lại mật khẩu!")]
        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không khớp!!!")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Không được để trống Email!")]
        [EmailAddress]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Không được để trống Mật khẩu!")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên!", MinimumLength = 6)]
        public string PasswordLogin { get; set; }
        //dang nhap tro lai trang detail
        public int? productId { get; set; }
    }
}
