namespace Assignment.Models
{
    public class ViewSanPham
    {
        public ViewSanPham()
        {

        }

        public ViewSanPham(int productDetailId, string productCode, string name, int quantity, int price, string? description, DateTime importDate, int manufactureYear, string category, string brand, string style, string national, bool status, List<string> imageLink)
        {
            ProductDetailId = productDetailId;
            ProductCode = productCode;
            Name = name;
            Quantity = quantity;
            Price = price;
            Description = description;
            ImportDate = importDate;
            ManufactureYear = manufactureYear;
            Category = category;
            Brand = brand;
            Style = style;
            National = national;
            Status = status;
            ImageLink=imageLink;
        }

        public int ProductDetailId { get; set; }
        public string ProductCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public DateTime ImportDate { get; set; }
        public int ManufactureYear { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Style { get; set; }
        public string National { get; set; }
        public bool Status { get; set; }
        public List<string> ImageLink { get; set; }
        public List<string> Color { get; set; }
    }
}
