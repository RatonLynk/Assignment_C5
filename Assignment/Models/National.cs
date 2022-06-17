using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class National
    {
        public National()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int NationalId { get; set; }
        public string NatinalName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
