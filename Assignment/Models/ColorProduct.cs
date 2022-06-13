using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class ColorProduct
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public bool Status { get; set; }

        public virtual Color Color { get; set; } = null!;
        public virtual ProductDetail Product { get; set; } = null!;
    }
}
