﻿using Microsoft.AspNetCore.Http;
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
        
    }
}
