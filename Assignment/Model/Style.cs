using System;
using System.Collections.Generic;

namespace Assignment.Model
{
    public partial class Style
    {
        public Style()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int StyleId { get; set; }
        public string StyleName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
