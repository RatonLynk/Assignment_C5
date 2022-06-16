using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            Carts = new HashSet<Cart>();
        }

        public int DeliveryTypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
