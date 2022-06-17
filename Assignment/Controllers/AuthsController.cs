using Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly C5_AssignmentContext _dbContext;

        public AuthsController(C5_AssignmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUsername([FromQuery] string username)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
            return new JsonResult(result);
        }

        // POST: api/Discountsapi/post-discount
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post-user")]
        public async Task<ActionResult<Discount>> PostDiscount(User user)
        {
            if (_dbContext.Users== null)
            {
                return Problem("Entity set 'C5_AssignmentContext.Discounts'  is null.");
            }
            if (_dbContext.Users.Count() == 0)
            {
                user.Id = 1;
            }
            else
            {
                user.Id = _dbContext.Users.Count() + 1;
            }
            user.RoleId = 2;
            _dbContext.Users.Add(user);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPost]
        [Route("check-phone")]
        public async Task<IActionResult> CheckPhone(UserForgotPasswordVM userForgot)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Phone == userForgot.Phone);
            if (user == null)
                return BadRequest("Phone is not found");
            return Ok(user);
        }
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordVM changePasswordVM)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == changePasswordVM.UserName && x.Phone == changePasswordVM.Phone);
            if (user == null)
                return BadRequest("User is not found");
            user.Password = changePasswordVM.ConfirmPassword;
            try
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool UserExists(int id)
        {
            return (_dbContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
