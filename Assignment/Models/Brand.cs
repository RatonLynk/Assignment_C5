using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Brand
    {
        public Brand()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }
        
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
