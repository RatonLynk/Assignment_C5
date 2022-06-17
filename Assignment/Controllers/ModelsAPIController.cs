using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        [HttpGet]
        [Route("OK")]
        public async Task<ActionResult<IEnumerable<ViewSanPham>>> ok()
        {

            var lstImg = _dbContext.Images;
            var lstViewSP= (from a in _dbContext.ProductDetails
                    join b in _dbContext.ColorProducts on a.ProductDetailId equals b.ProductId
                    join c in _dbContext.Colors on b.ColorId equals c.ColorId
                    join d in _dbContext.Brands on a.BrandId equals d.BrandId
                    join e in _dbContext.Styles on a.StyleId equals e.StyleId
                    join f in _dbContext.Nationals on a.NationalId equals f.NationalId
                    join g in _dbContext.Categories on a.CategoryId equals g.CategoryId
                    join h in _dbContext.DiscountProducts on a.ProductDetailId equals h.ProductId
                    join j in _dbContext.Discounts on h.DiscountId equals j.DiscountId
                  
                    select new ViewSanPham
                    {
                        ProductDetailId = a.ProductDetailId,
                        ProductCode = a.ProductCode,
                        Name = a.Name,
                        Quantity = a.Quantity,
                        Price = a.Price,
                        Description = a.Description,
                        ImportDate = a.ImportDate,
                        ManufactureYear = a.ManufactureYear,
                        Category = g.CategoryName,
                        Brand = d.BrandName,
                        Style = e.StyleName,
                        National = f.NatinalName,
                        Status = a.Status,
                    }).ToList();

            foreach(var x in lstViewSP)
            {
               var _lst= lstImg.Where(c => c.ProductId == x.ProductDetailId).Select(c=>c.Link).ToList();
                x.ImageLink = _lst;
            }

            return lstViewSP;

        }
        //public async Task<ActionResult<IEnumerable<Object>>> ok()
        //{
        //    return (from a in _dbContext.ProductDetails
        //            join b in _dbContext.ColorProducts on a.ProductDetailId equals b.ProductId
        //            join c in _dbContext.Colors on b.ColorId equals c.ColorId
        //            join d in _dbContext.Brands on a.BrandId equals d.BrandId
        //            join e in _dbContext.Styles on a.StyleId equals e.StyleId
        //            join f in _dbContext.Nationals on a.NationalId equals f.NationalId
        //            join g in _dbContext.Categories on a.CategoryId equals g.CategoryId
        //            join h in _dbContext.DiscountProducts on a.ProductDetailId equals h.ProductId
        //            join j in _dbContext.Discounts on h.DiscountId equals j.DiscountId
        //            where a.Status == true
        //            select new
        //            {
        //                id = a.ProductDetailId,
        //                code = a.ProductCode,
        //                name = a.Name,
        //                quantity = a.Quantity,
        //                price = a.Price,
        //                brand = d.BrandName,
        //                manufactureYear = a.ManufactureYear,
        //                category = g.CategoryName,
        //                national = f.NatinalName,
        //                style = e.StyleName,
        //                color = c.ColorName,
        //                discount = j.DiscountName
        //            }).ToList();
        //}
        [HttpPost("add-roles")]
        public async Task<string> AddRoles(Role rl)
        {
            if (rl==null)
            {
                return "List Null BullShit!";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rl = new Role
                    {
                        RoleId = _dbContext.Roles.Count(),
                        RoleName = rl.RoleName,
                        Status=rl.Status,
                    };
                    await _dbContext.Roles.AddAsync(rl);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return "BullShit!";
                }
            }
            return "Success";
        }

        [HttpPut("put/{id}")]
        public async Task<string> UpdateRole(int id, Role rl)
        {
            if (id != rl.RoleId)
            {
                return "Ngu";
            };
            _dbContext.Entry(rl).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
                return "Pass";
            }
            catch (DbUpdateConcurrencyException)
            {
                return "BullShit";
            }
        }
    }
}
