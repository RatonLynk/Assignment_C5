using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Model
{
    public partial class Category
    {
        public Category()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }
        [Range(0, 4)]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
