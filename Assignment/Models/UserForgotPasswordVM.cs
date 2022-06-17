using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class UserForgotPasswordVM
    {
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get;set; }
    }
}
