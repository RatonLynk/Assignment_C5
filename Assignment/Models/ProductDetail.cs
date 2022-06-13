using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            CartDetails = new HashSet<CartDetail>();
            ColorProducts = new HashSet<ColorProduct>();
            DiscountProducts = new HashSet<DiscountProduct>();
            Images = new HashSet<Image>();
        }

        public int ProductDetailId { get; set; }
        public string ProductCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public DateTime ImportDate { get; set; }
        public int ManufactureYear { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int StyleId { get; set; }
        public int NationalId { get; set; }
        public string? ImageId { get; set; }
        public bool Status { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual National National { get; set; } = null!;
        public virtual Style Style { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<ColorProduct> ColorProducts { get; set; }
        public virtual ICollection<DiscountProduct> DiscountProducts { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
