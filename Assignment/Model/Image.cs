using System;
using System.Collections.Generic;

namespace Assignment.Model
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; } = null!;
        public string Link { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ProductDetail Product { get; set; } = null!;
    }
}
