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
    public class ColorsController : Controller
    {
        private readonly C5_AssignmentContext _context;

        public ColorsController(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: Colors
        public async Task<IActionResult> Index()
        {
              return _context.Colors != null ? 
                          View(await _context.Colors.ToListAsync()) :
                          Problem("Entity set 'C5_AssignmentContext.Colors'  is null.");
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.ColorId == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            //int id = _context.Colors.ToList().Count();
            //ViewData["id"] = id;
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorId,ColorName,Status")] Color color)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                //color.ColorId = _context.Colors.Count() + 1;
                var jsondata = client.PostAsJsonAsync("api/ColorsAPI/post-colors", color).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorId,ColorName,Status")] Color color)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PutAsJsonAsync("api/ColorsAPI/put/" + id, color).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.ColorId == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Color color = new Color();
            color = _context.Colors.FirstOrDefault(c => c.ColorId == id);
            if (color != null)
            {
                color.Status = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PutAsJsonAsync("api/ColorsAPI/color-delete/" + id, color).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(); 
        }

        private bool ColorExists(int id)
        {
          return (_context.Colors?.Any(e => e.ColorId == id)).GetValueOrDefault();
        }
    }
}
