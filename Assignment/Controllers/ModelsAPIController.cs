using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment.Model;
using Microsoft.EntityFrameworkCore;
namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsAPIController : ControllerBase
    {
        List<QLSP> _lstQLSP;

        C5_AssignmentContext _dbContext = new C5_AssignmentContext();
        public ModelsAPIController(C5_AssignmentContext dbContext)
        {

            _dbContext = dbContext;
            _lstQLSP=new List<QLSP>();
        }
        
        //Get all models API
        
        [HttpGet]
        [Route("api-get-all")]
        public async Task<IEnumerable<QLSP>> GetAll()
        {
            _lstQLSP = (from a in _dbContext.Products.ToList()
                        join b in _dbContext.ProductDetails.ToList() on a.ProductId equals b.ProductDetailId
                        join c in _dbContext.ColorProducts.ToList() on a.ProductId equals c.ProductId
                        join d in _dbContext.Colors.ToList() on c.ColorId equals d.ColorId
                        join e in _dbContext.Brands.ToList() on b.BrandId equals e.BrandId
                        join f in _dbContext.Styles.ToList() on b.StyleId equals f.StyleId
                        join g in _dbContext.Nationals.ToList() on b.NationalId equals g.NationalId
                        join h in _dbContext.Categories.ToList() on b.CategoryId equals h.CategoryId
                        join i in _dbContext.DiscountProducts.ToList() on b.ProductDetailId equals i.ProductId
                        join j in _dbContext.Discounts.ToList() on i.DiscountId equals j.DiscountId
                        join k in _dbContext.Images.ToList() on b.ProductDetailId equals k.ProductId
                        select new QLSP
                        {
                            Product = a,
                            ProductDetail = b,
                            ColorProduct = c,
                            Color = d,
                            Brand = e,
                            Style = f,
                            National = g,
                            Category = h,
                            DiscountProduct = i,
                            Discount = j,
                            Image = k
                        }
                      ).ToList();
            return _lstQLSP;
        }
    }
}
