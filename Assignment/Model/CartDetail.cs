using System;
using System.Collections.Generic;

namespace Assignment.Model
{
    public partial class CartDetail
    {
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual ProductDetail Product { get; set; } = null!;
    }
}
