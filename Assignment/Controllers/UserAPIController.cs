using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        C5_AssignmentContext _dbContext = new C5_AssignmentContext();
        public UserAPIController(C5_AssignmentContext dbContext)
        {

            _dbContext = dbContext;
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            return await _dbContext.Users.ToListAsync();
        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Entity Null BullShit!");
            }
            _dbContext.Users.Add(user);
            try
            {
                if (UserIDExist(user.Id))
                {
                    return BadRequest("Conflict");
                }
                if (UserNameExist(user.Username))
                {
                    return BadRequest("Existing username");
                }
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("put/{id}")]
        public async Task<string> UpdateUser(int id,User user)
        {
            if (id != user.Id)
            {
                return "Ngu";
            };
            _dbContext.Entry(user).State = EntityState.Modified;
            try
            {
                if (UserNameExist(user.Username))
                {
                    return "Existing username";
                }
                await _dbContext.SaveChangesAsync();
                return "Pass";
            }
            catch (DbUpdateConcurrencyException)
            {
                return "BullShit";
            }
        }
        // DELETE: api/Styles/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        private bool UserIDExist(int id)
        {
            return (_dbContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool UserNameExist(string username)
        {
            return (_dbContext.Users?.Any(e => e.Username == username)).GetValueOrDefault();
        }
    }
}
