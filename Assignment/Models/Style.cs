using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public partial class Style
    {
        public Style()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }
        
        public int StyleId { get; set; }
        [Required]
        public string StyleName { get; set; } = null!;
        
        public bool Status { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
