using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Model
{
    public partial class Image
    {
        [Range(0, 4)]
        public int ImageId { get; set; }
        [Range(0, 4)]
        public int ProductId { get; set; }
        [Required]
        public string ImageName { get; set; } = null!;
        public string Link { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ProductDetail Product { get; set; } = null!;
    }
}
