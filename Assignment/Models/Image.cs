using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string Link { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ProductDetail Product { get; set; } = null!;
    }
}
