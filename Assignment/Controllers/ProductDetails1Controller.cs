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
            User user;
            if (HttpContext.Session.GetString("username") == null || HttpContext.Session.GetString("username") == "")
            {
                

                
                List<CartDetail>? cartItems = new List<CartDetail>();
                if (HttpContext.Session.GetString("Cart") == null || HttpContext.Session.GetString("Cart").Length == 0)
                {
                    cartItems = new List<CartDetail>();
                }
                else
                {
                    string jsonCart = HttpContext.Session.GetString("Cart");
                    cartItems = JsonConvert.DeserializeObject<List<CartDetail>>(jsonCart);
                }
                if (cartItems.FirstOrDefault(c=>c.ProductId == prodID) != null)
                {
                    cartItems.FirstOrDefault(c => c.ProductId == prodID).Quantity += buyAmount;
                } else
                {
                    Item.CartId = 0;
                    Item.Status = true;
                    Item.Quantity = buyAmount;
                    Item.ProductId = prodID;
                    Item.ColorID = colorID;
                    cartItems.Add(Item);
                }
                
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));

            }
            else
            {
                user = _context.Users.FirstOrDefault(u => u.Username == HttpContext.Session.GetString("username"));
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
                if (cart.)
                {

                }
                    await httpClient.PostAsJsonAsync("CartDetails", Item);
                CartDetailsController control = new CartDetailsController(_context);
                control.PostCartDetail(Item);
            }
            
            return RedirectToAction("Cart");
        }

        [HttpPost, ActionName("Remove")]
        [Route("Products/Remove/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            if (HttpContext.Session.GetString("username") == null || HttpContext.Session.GetString("username") == "") 
            {
                string? jsonCart = HttpContext.Session.GetString("Cart");
                List<CartDetail>? cartItems = JsonConvert.DeserializeObject<List<CartDetail>>(jsonCart);
                cartItems.FirstOrDefault(i => i.CartDetailId == id).Quantity = 0;
                HttpContext.Session.SetString("Cart", cartItems.ToString());
            }
            else
            {
                CartDetail item = _context.CartDetails.FirstOrDefault(i => i.CartDetailId == id);
                item.Quantity = 0;
                _context.CartDetails.Update(item);
                await _context.SaveChangesAsync();
            }
                 
            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> Cart()
        {
            User? user = new User();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7110/api/ModelsAPI/");
            var JsonConnect = client.GetAsync("ok").Result;
            if (HttpContext.Session.GetString("username") == null || HttpContext.Session.GetString("username") == "")
            {
                

                 string? jsonCart = HttpContext.Session.GetString("Cart");
                List<CartDetail>? cartItems = new List<CartDetail>();
                if (jsonCart != null && jsonCart.Length != 0)
                {
                    List<ViewSanPham> products = new List<ViewSanPham>();
                    cartItems = JsonConvert.DeserializeObject<List<CartDetail>>(jsonCart);
                    string Data = JsonConnect.Content.ReadAsStringAsync().Result;
                    List<ViewSanPham> lstView = JsonConvert.DeserializeObject<List<ViewSanPham>>(Data);
                    foreach (CartDetail item in cartItems)
                    {
                        products.Add(lstView.FirstOrDefault(p => p.ProductDetailId == item.ProductId));
                    }
                    ViewData["CartProducts"] = products;
                }

                return View(cartItems);

            } else
            {
                user = _context.Users.FirstOrDefault(u => u.Username == HttpContext.Session.GetString("username"));
                Cart? CartForLoading = _context.Carts.FirstOrDefault(c => c.UserId == user.Id && c.Status);
                if (CartForLoading == null)
                {
                    return View(new List<CartDetail>());
                } else
                {
                    List<CartDetail> LoadCartItems = _context.CartDetails.Where(i => i.CartId == CartForLoading.CartId && i.Quantity > 0).ToList();
                    List<ViewSanPham> products = new List<ViewSanPham>();
                    if (LoadCartItems.Count > 0 && LoadCartItems != null)
                    {
                        string JsonData = JsonConnect.Content.ReadAsStringAsync().Result;
                        List<ViewSanPham> lstView = JsonConvert.DeserializeObject<List<ViewSanPham>>(JsonData);
                        foreach (CartDetail item in LoadCartItems)
                        {
                            products.Add(lstView.FirstOrDefault(p => p.ProductDetailId == item.ProductId));
                        }
                        ViewData["CartProducts"] = products;
                    }
                    return View(LoadCartItems);
                }
            }
            return NoContent();
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
