using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalsAPIController : ControllerBase
    {
        private readonly C5_AssignmentContext _context;

        public NationalsAPIController(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: api/NationalsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<National>>> GetNationals()
        {
            if (_context.Nationals == null)
            {
                return NotFound();
            }
            return await _context.Nationals.ToListAsync();
        }

        // GET: api/NationalsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<National>> GetNational(
            [RegularExpression(@"^[a-zA-Z]+$",
            ErrorMessage = "Name is incorrect")] int id)
        {
            if (_context.Nationals == null)
            {
                return NotFound();
            }
            var national = await _context.Nationals.FindAsync(id);

            if (national == null)
            {
                return NotFound();
            }

            return national;
        }

        // PUT: api/NationalsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("put{id}")]
        public async Task<IActionResult> PutNational(int id, National national)
        {
            if (id != national.NationalId)
            {
                return BadRequest();
            }

            _context.Entry(national).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NationalsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("post-nationals")]
        public async Task<ActionResult<National>> PostNational(National national)
        {
            if (_context.Nationals == null)
            {
                return Problem("Entity set 'C5_AssignmentContext.Nationals'  is null.");
            }
            _context.Nationals.Add(national);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NationalExists(national.NationalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNational", new { id = national.NationalId }, national);
        }

        // DELETE: api/NationalsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNational(int id)
        {
            if (_context.Nationals == null)
            {
                return NotFound();
            }
            var national = await _context.Nationals.FindAsync(id);
            if (national == null)
            {
                return NotFound();
            }

            _context.Nationals.Remove(national);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NationalExists(int id)
        {
            return (_context.Nationals?.Any(e => e.NationalId == id)).GetValueOrDefault();
        }
        [HttpPut("national-delete/{id}")]
        public async Task<IActionResult> FakeDelete(int id, National national)
        {
            if (id != national.NationalId)
            {
                return BadRequest();
            }

            _context.Entry(national).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
