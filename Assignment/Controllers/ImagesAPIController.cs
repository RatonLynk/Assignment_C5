using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using Newtonsoft.Json;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesAPIController : ControllerBase
    {
        private readonly C5_AssignmentContext _context;

        public ImagesAPIController(C5_AssignmentContext context)
        {
            _context = context;
        }

        // GET: api/ImagesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
          if (_context.Images == null)
          {
              return NotFound();
          }
            return await _context.Images.ToListAsync();
        }

        // GET: api/ImagesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
          if (_context.Images == null)
          {
              return NotFound();
          }
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // PUT: api/ImagesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutImage(int id, Image image)
        {
            if (id != image.ImageId)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        // POST: api/ImagesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("post-images")]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
          if (_context.Images == null)
          {
              return Problem("Entity set 'C5_AssignmentContext.Images'  is null.");
          }
            _context.Images.Add(image);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImageExists(image.ImageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImage", new { id = image.ImageId }, image);
        }

        // DELETE: api/ImagesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            if (_context.Images == null)
            {
                return NotFound();
            }
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageExists(int id)
        {
            return (_context.Images?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
        [HttpPut("image-delete/{id}")]
        public async Task<IActionResult> FakeDelete(int id, Image image)
        {
            if (id != image.ImageId)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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
