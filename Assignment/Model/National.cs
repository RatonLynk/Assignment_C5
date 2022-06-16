using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Model
{
    public partial class National
    {
        public National()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }
        [Range(0, 4)]
        public int NationalId { get; set; }
        
        [Required]
        public string NatinalName { get; set; } = null!;
        
        public bool Status { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
