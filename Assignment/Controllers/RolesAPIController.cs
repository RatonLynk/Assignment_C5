using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesAPIController : ControllerBase
    {
        C5_AssignmentContext _dbContext = new C5_AssignmentContext();
        public RolesAPIController(C5_AssignmentContext dbContext)
        {

            _dbContext = dbContext;
        }
        [HttpGet("get-roles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            if (_dbContext.Roles==null)
            {
                return NotFound();
            }
            return await _dbContext.Roles.ToListAsync();
        }
        [HttpPost("add-roles")]
        public async Task<string> AddRoles(Role rl)
        {
            if (rl == null)
            {
                return "List Null BullShit!";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rl = new Role
                    {
                        RoleId = _dbContext.Roles.Count() + 1,
                        RoleName = rl.RoleName,
                    };
                    await _dbContext.Roles.AddAsync(rl);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return "BullShit!";
                }
            }
            return "Success";
        }

        [HttpPut("put/{id}")]
        public async Task<string> UpdateRole(int id, Role rl)
        {
            if (id != rl.RoleId)
            {
                return "Ngu";
            };
            _dbContext.Entry(rl).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
                return "Pass";
            }
            catch (DbUpdateConcurrencyException)
            {
                return "BullShit";
            }
        }



    }
}
