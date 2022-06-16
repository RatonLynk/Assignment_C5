using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public int CartId { get; set; }
        public int? UserId { get; set; }
        public int? Total { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateConfirmed { get; set; }
        public bool Status { get; set; }
        public int? DeliveryTypeId { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public virtual DeliveryType? DeliveryType { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
