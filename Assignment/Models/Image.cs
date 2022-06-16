using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public partial class Image
    {
        [Range(0, 4)]
        public int ImageId { get; set; }
        [Range(0, 4)]
        public int ProductId { get; set; }
        public string Link { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ProductDetail Product { get; set; } = null!;
    }
}
