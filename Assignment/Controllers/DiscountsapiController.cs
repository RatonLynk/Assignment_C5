using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsapiController : ControllerBase
    {
        private readonly C5_AssignmentContext _context;

        public DiscountsapiController(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: api/Discountsapi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
          if (_context.Discounts == null)
          {
              return NotFound();
          }
            return await _context.Discounts.ToListAsync();
        }

        // GET: api/Discountsapi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscount(int id)
        {
          if (_context.Discounts == null)
          {
              return NotFound();
          }
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            return discount;
        }

        // PUT: api/Discountsapi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutDiscount(int id, Discount discount)
        {
            if (id != discount.DiscountId)
            {
                return BadRequest();
            }

            _context.Entry(discount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
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

        // POST: api/Discountsapi/post-discount
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post-discount")]
        public async Task<ActionResult<Discount>> PostDiscount(Discount discount)
        {
          if (_context.Discounts == null)
          {
              return Problem("Entity set 'C5_AssignmentContext.Discounts'  is null.");
          }
            if (_context.Discounts.Count() == 0)
            {
                discount.DiscountId = 1;
            }
            else
            {
                discount.DiscountId = _context.Discounts.Count() + 1;
            }

            _context.Discounts.Add(discount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiscountExists(discount.DiscountId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDiscount", new { id = discount.DiscountId }, discount);
        }

        // DELETE: api/Discountsapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            if (_context.Discounts == null)
            {
                return NotFound();
            }
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool DiscountExists(int id)
        {
            return (_context.Discounts?.Any(e => e.DiscountId == id)).GetValueOrDefault();
        }
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> FakeDelete(int id, Discount discount)
        {
            if (id != discount.DiscountId)
            {
                return BadRequest();
            }

            _context.Entry(discount).State = EntityState.Modified;

            try
            {
                discount.Status = false;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
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
