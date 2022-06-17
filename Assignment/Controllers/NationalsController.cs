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
    public class NationalsController : Controller
    {
        private readonly C5_AssignmentContext _context;

        public NationalsController(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: Nationals
        public async Task<IActionResult> Index()
        {
              return _context.Nationals != null ? 
                          View(await _context.Nationals.ToListAsync()) :
                          Problem("Entity set 'C5_AssignmentContext.Nationals'  is null.");
        }

        // GET: Nationals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nationals == null)
            {
                return NotFound();
            }

            var national = await _context.Nationals
                .FirstOrDefaultAsync(m => m.NationalId == id);
            if (national == null)
            {
                return NotFound();
            }

            return View(national);
        }

        // GET: Nationals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nationals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NationalId,NatinalName,Status")] National national)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PostAsJsonAsync("api/NationalsAPI/post-nationals", national).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View(national);
        }

        // GET: Nationals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nationals == null)
            {
                return NotFound();
            }

            var national = await _context.Nationals.FindAsync(id);
            if (national == null)
            {
                return NotFound();
            }
            return View(national);
        }

        // POST: Nationals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NationalId,NatinalName,Status")] National national)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PutAsJsonAsync("api/NationalsAPI/put/" + id, national).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }        
            return View(national);
        }

        // GET: Nationals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nationals == null)
            {
                return NotFound();
            }

            var national = await _context.Nationals
                .FirstOrDefaultAsync(m => m.NationalId == id);
            if (national == null)
            {
                return NotFound();
            }

            return View(national);
        }

        // POST: Nationals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            National national = new National();
            national = _context.Nationals.FirstOrDefault(c => c.NationalId == id);
            if (national != null)
            {
                national.Status = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PutAsJsonAsync("api/NationalsAPI/national-delete/" + id, national).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private bool NationalExists(int id)
        {
          return (_context.Nationals?.Any(e => e.NationalId == id)).GetValueOrDefault();
        }
    }
}
