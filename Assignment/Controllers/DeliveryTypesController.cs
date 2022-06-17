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
    public class DeliveryTypesController : Controller
    {
        private readonly C5_AssignmentContext _context;

        public DeliveryTypesController(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: DeliveryTypes
        public async Task<IActionResult> Index()
        {
              return _context.DeliveryTypes != null ? 
                          View(await _context.DeliveryTypes.ToListAsync()) :
                          Problem("Entity set 'C5_AssignmentContext.DeliveryTypes'  is null.");
        }

        // GET: DeliveryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryTypes == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes
                .FirstOrDefaultAsync(m => m.DeliveryTypeId == id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return View(deliveryType);
        }

        // GET: DeliveryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeliveryType deliveryType)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PostAsJsonAsync("api/DeliveryTypes1/post-delivery", deliveryType).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryType);
        }

        // GET: DeliveryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeliveryTypes == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes.FindAsync(id);
            if (deliveryType == null)
            {
                return NotFound();
            }
            return View(deliveryType);
        }

        // POST: DeliveryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryTypeId,TypeName,Status")] DeliveryType deliveryType)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PutAsJsonAsync("api/StylesAPI/put/" + id, deliveryType).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryType);
        }

        // GET: DeliveryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryTypes == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes
                .FirstOrDefaultAsync(m => m.DeliveryTypeId == id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return View(deliveryType);
        }

        // POST: DeliveryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            DeliveryType delivery = new DeliveryType();
            delivery = _context.DeliveryTypes.FirstOrDefault(c => c.DeliveryTypeId == id);
            if (delivery != null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.DeleteAsync("api/DeliveryTypes1/delete/" + id).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private bool DeliveryTypeExists(int id)
        {
          return (_context.DeliveryTypes?.Any(e => e.DeliveryTypeId == id)).GetValueOrDefault();
        }
    }
}
