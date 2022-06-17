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
    public class DeliveryTypes1Controller : ControllerBase
    {
        private readonly C5_AssignmentContext _context;

        public DeliveryTypes1Controller(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: api/DeliveryTypes1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryType>>> GetDeliveryTypes()
        {
            if (_context.DeliveryTypes == null)
            {
                return NotFound();
            }
            return await _context.DeliveryTypes.ToListAsync();
        }

        // GET: api/DeliveryTypes1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryType>> GetDeliveryType(int id)
        {
            if (_context.DeliveryTypes == null)
            {
                return NotFound();
            }
            var deliveryType = await _context.DeliveryTypes.FindAsync(id);

            if (deliveryType == null)
            {
                return NotFound();
            }

            return deliveryType;
        }

        // PUT: api/DeliveryTypes1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutDeliveryType(int id, DeliveryType deliveryType)
        {
            if (id != deliveryType.DeliveryTypeId)
            {
                return BadRequest();
            }

            _context.Entry(deliveryType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTypeExists(id))
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

        // POST: api/DeliveryTypes1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post-delivery")]
        public async Task<ActionResult<DeliveryType>> PostDeliveryType(DeliveryType deliveryType)
        {
            if (_context.DeliveryTypes == null)
            {
                return Problem("Entity set 'C5_AssignmentContext.DeliveryTypes'  is null.");
            }
            if (_context.DeliveryTypes.Count() == 0)
            {
                deliveryType.DeliveryTypeId = 1;
            }
            else
            {
                deliveryType.DeliveryTypeId = _context.DeliveryTypes.Count() + 1;
            }
            _context.DeliveryTypes.Add(deliveryType);
            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeliveryTypeExists(deliveryType.DeliveryTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeliveryType", new { id = deliveryType.DeliveryTypeId }, deliveryType);
        }

        // DELETE: api/DeliveryTypes1/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDeliveryType(int id)
        {
            if (_context.DeliveryTypes == null)
            {
                return NotFound();
            }
            var deliveryType = await _context.DeliveryTypes.FindAsync(id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            _context.DeliveryTypes.Remove(deliveryType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> FakeDelete(int id, DeliveryType deliveryType)
        {
            if (id != deliveryType.DeliveryTypeId)
            {
                return BadRequest();
            }

            _context.Entry(deliveryType).State = EntityState.Modified;

            try
            {
                deliveryType.Status = false;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTypeExists(id))
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
        private bool DeliveryTypeExists(int id)
        {
            return (_context.DeliveryTypes?.Any(e => e.DeliveryTypeId == id)).GetValueOrDefault();
        }
    } 
}
