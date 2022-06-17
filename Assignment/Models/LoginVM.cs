using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Vui lòng không bỏ trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống")]
        public string Password { get; set; }
    }
}
