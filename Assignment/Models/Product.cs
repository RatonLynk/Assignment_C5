using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ProductDetail ProductNavigation { get; set; } = null!;
    }
}
