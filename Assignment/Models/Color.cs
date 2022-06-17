using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Color
    {
        public Color()
        {
            ColorProducts = new HashSet<ColorProduct>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<ColorProduct> ColorProducts { get; set; }
    }
}
