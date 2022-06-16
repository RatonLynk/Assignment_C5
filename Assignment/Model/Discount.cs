using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Model
{
    public partial class Discount
    {
        public Discount()
        {
            DiscountProducts = new HashSet<DiscountProduct>();
        }
        [Range(0, 4)]
        public int DiscountId { get; set; }
        [Required]
        public string DiscountName { get; set; } = null!;
        [Required]
        public int Percentage { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<DiscountProduct> DiscountProducts { get; set; }
    }
}
