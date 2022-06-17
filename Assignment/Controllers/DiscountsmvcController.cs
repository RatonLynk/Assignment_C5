using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;

namespace Assignment.Controllers
{
    public class DiscountsmvcController : Controller
    {
        private readonly C5_AssignmentContext _context;

        public DiscountsmvcController(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: Discountsmvc
        public async Task<IActionResult> Index()
        {
              return _context.Discounts != null ? 
                          View(await _context.Discounts.ToListAsync()) :
                          Problem("Entity set 'C5_AssignmentContext.Discounts'  is null.");
        }

        // GET: Discountsmvc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .FirstOrDefaultAsync(m => m.DiscountId == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Discountsmvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discountsmvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discount discount)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PostAsJsonAsync("api/Discountsapi/post-discount", discount).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        // GET: Discountsmvc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);
        }

        // POST: Discountsmvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscountId,DiscountName,Percentage,StartDate,EndDate,Status")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PutAsJsonAsync("api/Discountsapi/put/"+id, discount).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        // GET: Discountsmvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .FirstOrDefaultAsync(m => m.DiscountId == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Discountsmvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Discount discount=new Discount();
            discount = _context.Discounts.FirstOrDefault(c => c.DiscountId == id);
            if (ModelState.IsValid)
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PutAsJsonAsync("api/Discountsapi/delete/" + id,discount).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        private bool DiscountExists(int id)
        {
          return (_context.Discounts?.Any(e => e.DiscountId == id)).GetValueOrDefault();
        }
    }
}
