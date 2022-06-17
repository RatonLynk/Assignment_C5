using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class UserChangePasswordVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng không bỏ trống")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
