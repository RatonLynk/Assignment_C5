﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class RolesController : Controller
    {
        private readonly C5_AssignmentContext _context;

        public RolesController(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
              return _context.Roles != null ? 
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'C5_AssignmentContext.Roles'  is null.");
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleName,Status")] Role role)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110");
                var jsonReport = client.PostAsJsonAsync("api/ModelsAPI/add-roles", role).Result;
                string check= await jsonReport.Content.ReadAsStringAsync();
                if (check=="Success")
                {
                    return RedirectToAction("Index");
                }
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleName,Status")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new System.Uri("https://localhost:7156/");
                    var jsondata = client.PutAsJsonAsync("api/ModelsAPI/" + id + "/update-roles", role).Result;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
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
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'C5_AssignmentContext.Roles'  is null.");
            }
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                if (role.Status == true)
                {
                    role.Status = false;
                }
                HttpClient client =new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7156/");
                var jsonRp = client.PutAsJsonAsync("api/ModelsAPI/" + id + "/update-role",role).Result;
                string rt = await jsonRp.Content.ReadAsStringAsync();
                if (rt=="Pass")
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
          return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
