using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            Carts = new HashSet<Cart>();
        }

        public int DeliveryTypeId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống ")]
        [MinLength(3, ErrorMessage = "name discount không được nhỏ hơn 3 kí tự")]
        public string TypeName { get; set; } = null!;

        public bool Status { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
