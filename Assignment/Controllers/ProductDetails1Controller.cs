using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Assignment.Controllers
{
    public class ProductDetails1Controller : Controller
    {
        private readonly C5_AssignmentContext _context;

        public ProductDetails1Controller(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: ProductDetails1

        [HttpPost, ActionName("AddToCart")]
        public async Task<IActionResult> AddToCart(int buyAmount, int prodID, int colorID)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7110");
            CartDetail Item = new CartDetail();

            //User? user = _context.Users.First(u => u.Username == HttpContext.Session.GetString("username"));
            User user = new User();
            user.Id = 0;
            //if (user == null)
            //{
            //    string? jsonCart = HttpContext.Session.GetString("Cart");
            //    List<CartDetail>? cartItems = new List<CartDetail>();
            //    if (jsonCart == null || jsonCart.Length == 0)
            //    {
            //        cartItems = new List<CartDetail>();
            //    }
            //    else
            //    {
            //        cartItems = JsonConvert.DeserializeObject<List<CartDetail>>(jsonCart);
            //    }

            //    Item.CartId = 0;
            //    Item.Status = true;
            //    Item.Quantity = buyAmount;
            //    Item.ProductId = prodID;
            //    Item.ColorID = colorID;
            //    cartItems.Add(Item);
            //    HttpContext.Session.SetString("Cart", cartItems.ToString());

            //} else
            {
                Cart? cart = _context.Carts.FirstOrDefault(c => c.UserId == user.Id && c.Status == true);
                if (cart == null)
                {
                    cart = new Cart();
                    cart.CartId = _context.Carts.ToList().Count();
                    cart.DateCreated = DateTime.Now;
                    cart.UserId = user.Id;
                    cart.Status = true;
                    await httpClient.PostAsJsonAsync("api/Carts/Add-Cart", cart);
                }
                Item.Status = true;
                Item.CartId = cart.CartId;
                Item.ProductId = prodID;
                Item.Quantity = buyAmount;
                Item.ColorID = colorID;
                Item.CartDetailId = _context.CartDetails.ToList().Count();
                await httpClient.PostAsJsonAsync("api/CartDetails/Add-Item", Item);

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7110/api/ModelsAPI/");
            var JsonConnect = client.GetAsync("ok").Result;
            string path = "https://localhost:7110/api/ModelsAPI/ok";
            object productDetail=getProductDetail(path);
            string JsonData = JsonConnect.Content.ReadAsStringAsync().Result;

            //JObject jObject=JObject.Parse(productDetail.ToString());
            ViewData["data"] = JsonData;

            var model = JsonConvert.DeserializeObject<List<ViewSanPham>>(JsonData);
            //ViewBag.data = jObject["results"];
            return View(model);
        }
        public object getProductDetail(string path)
        {
            using(WebClient webClient=new WebClient())
            {
                return JsonConvert.DeserializeObject<object>(
                    webClient.DownloadString(path));
            }
        }
        // GET: ProductDetails1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductDetails == null)
            {
                return NotFound();
            }

            var productDetail = await _context.ProductDetails
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.National)
                .Include(p => p.Style)
                .FirstOrDefaultAsync(m => m.ProductDetailId == id);
            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }

        // GET: ProductDetails1/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["NationalId"] = new SelectList(_context.Nationals, "NationalId", "NationalId");
            ViewData["StyleId"] = new SelectList(_context.Styles, "StyleId", "StyleId");
            return View();
        }

        // POST: ProductDetails1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductDetailId,ProductCode,Name,Quantity,Price,Description,ImportDate,ManufactureYear,CategoryId,BrandId,StyleId,NationalId,ImageId,Status")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", productDetail.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", productDetail.CategoryId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "NationalId", "NationalId", productDetail.NationalId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "StyleId", "StyleId", productDetail.StyleId);
            return View(productDetail);
        }

        // GET: ProductDetails1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductDetails == null)
            {
                return NotFound();
            }

            var productDetail = await _context.ProductDetails.FindAsync(id);
            if (productDetail == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", productDetail.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", productDetail.CategoryId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "NationalId", "NationalId", productDetail.NationalId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "StyleId", "StyleId", productDetail.StyleId);
            return View(productDetail);
        }

        // POST: ProductDetails1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductDetailId,ProductCode,Name,Quantity,Price,Description,ImportDate,ManufactureYear,CategoryId,BrandId,StyleId,NationalId,ImageId,Status")] ProductDetail productDetail)
        {
            if (id != productDetail.ProductDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductDetailExists(productDetail.ProductDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", productDetail.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", productDetail.CategoryId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "NationalId", "NationalId", productDetail.NationalId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "StyleId", "StyleId", productDetail.StyleId);
            return View(productDetail);
        }

        // GET: ProductDetails1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductDetails == null)
            {
                return NotFound();
            }

            var productDetail = await _context.ProductDetails
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.National)
                .Include(p => p.Style)
                .FirstOrDefaultAsync(m => m.ProductDetailId == id);
            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }

        // POST: ProductDetails1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductDetails == null)
            {
                return Problem("Entity set 'C5_AssignmentContext.ProductDetails'  is null.");
            }
            var productDetail = await _context.ProductDetails.FindAsync(id);
            if (productDetail != null)
            {
                _context.ProductDetails.Remove(productDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductDetailExists(int id)
        {
          return (_context.ProductDetails?.Any(e => e.ProductDetailId == id)).GetValueOrDefault();
        }
    }
}
