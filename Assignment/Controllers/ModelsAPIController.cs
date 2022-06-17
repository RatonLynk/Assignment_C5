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

            C5_AssignmentContext _dbContext = new C5_AssignmentContext();
            public ModelsAPIController(C5_AssignmentContext dbContext)
            {

                _dbContext = dbContext;
            }

            //Get all models API

            [HttpGet]
            [Route("OK")]
            public async Task<ActionResult<List<ViewSanPham>>> ok()
            {

                var lstImg = _dbContext.Images;


                var lstViewSP = (from a in _dbContext.ProductDetails
                                 join d in _dbContext.Brands on a.BrandId equals d.BrandId
                                 join e in _dbContext.Styles on a.StyleId equals e.StyleId
                                 join f in _dbContext.Nationals on a.NationalId equals f.NationalId
                                 join g in _dbContext.Categories on a.CategoryId equals g.CategoryId

                                 select new ViewSanPham()
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

                foreach (var x in lstViewSP)
                {
                    var _lst = lstImg.Where(c => c.ProductId == x.ProductDetailId).Select(c => c.Link).ToList();
                    x.ImageLink = _lst;
                }

                foreach (var x in lstViewSP)
                {
                    var lst = _dbContext.ColorProducts.Where(c => c.ProductId == x.ProductDetailId).ToList();
                    List<string> couloire = _dbContext.Colors.Where(c => lst.Select(c => c.ColorId).Contains(c.ColorId)).Select(c => c.ColorName).ToList();
                    x.Color = couloire;
                }

                return lstViewSP;

        }
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
                            RoleId = _dbContext.Roles.Count() + 1,
                            RoleName = rl.RoleName,
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

            [HttpPut("{id}/update-roles")]
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
