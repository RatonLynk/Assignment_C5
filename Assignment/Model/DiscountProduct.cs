using System;
using System.Collections.Generic;

namespace Assignment.Model
{
    public partial class DiscountProduct
    {
        public int ProductId { get; set; }
        public int DiscountId { get; set; }
        public bool Status { get; set; }

        public virtual Discount Discount { get; set; } = null!;
        public virtual ProductDetail Product { get; set; } = null!;
    }
}
