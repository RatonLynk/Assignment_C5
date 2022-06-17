using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng không bỏ trống")]
        [MinLength(3, ErrorMessage = "username không được nhỏ hơn 5 kí tự")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng không bỏ trống")]
        [MinLength(8, ErrorMessage = "passwword phải lớn hơn 8 kí tự ")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng không bỏ trống")]
        [MinLength(8, ErrorMessage = "FullName không được nhỏ hơn 5 kí tự")]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng không bỏ trống")]
        [MinLength(10, ErrorMessage = "sdt phải là 10 số")]
        [MaxLength(10, ErrorMessage = "sdt phải là 10 số")]
        [Phone(ErrorMessage ="Phải là số điện thoại ")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng không bỏ trống")]
        public string Address { get; set; } = null!;
        public int RoleId { get; set; }
        public bool Status { get; set; }

        public virtual Role? Role { get; set; } = null!;
        public virtual ICollection<Cart>? Carts { get; set; }
    }
}
