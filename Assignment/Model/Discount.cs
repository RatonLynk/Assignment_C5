using System;
using System.Collections.Generic;

namespace Assignment.Model
{
    public partial class Discount
    {
        public Discount()
        {
            DiscountProducts = new HashSet<DiscountProduct>();
        }

        public int DiscountId { get; set; }
        public string DiscountName { get; set; } = null!;
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<DiscountProduct> DiscountProducts { get; set; }
    }
}
