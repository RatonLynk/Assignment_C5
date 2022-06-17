using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public partial class Discount
    {
        public Discount()
        {
            DiscountProducts = new HashSet<DiscountProduct>();
        }

        public int DiscountId { get; set; }
        [Required(ErrorMessage ="Không được bỏ trống ")]
        [MinLength(3,ErrorMessage ="name discount không được nhỏ hơn 3 kí tự")]
        public string DiscountName { get; set; } = null!;
        [Required(ErrorMessage = "Không được bỏ trống ")]
        public int Percentage { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống ")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống ")]
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<DiscountProduct> DiscountProducts { get; set; }
    }
}
