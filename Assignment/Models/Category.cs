using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
