using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public partial class Color
    {
        public Color()
        {
            ColorProducts = new HashSet<ColorProduct>();
        }
        [Range(0, 4)]
        public int ColorId { get; set; }
        [Required]
        public string ColorName { get; set; } = null!;
        
        public bool Status { get; set; }

        public virtual ICollection<ColorProduct> ColorProducts { get; set; }
    }
}
