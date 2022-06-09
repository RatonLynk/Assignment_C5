using System;
using System.Collections.Generic;

namespace Assignment.Model
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int RoleId { get; set; }
        public bool Status { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Cart Cart { get; set; } = null!;
    }
}
