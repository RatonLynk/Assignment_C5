using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment.Model;
using Microsoft.EntityFrameworkCore;
namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsAPI : ControllerBase
    {
        C5_AssignmentContext _dbContext = new C5_AssignmentContext();
        public ModelsAPI(C5_AssignmentContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Get all models API
        [HttpGet]
        [Route("api-get-list-Brand")]
        public async Task<ActionResult<IEnumerable<Brand>>> GetLstBrand()
        {
            if (_dbContext.Brands.Count() > 0)
            {
                return await _dbContext.Brands.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api-get-list-Categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetLstCategories()
        {
            if (_dbContext.Categories.Count() > 0)
            {
                return await _dbContext.Categories.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api-get-list-Images")]
        public async Task<ActionResult<IEnumerable<Image>>> GetLstImages()
        {
            if (_dbContext.Images.Count() > 0)
            {
                return await _dbContext.Images.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api-get-list-Styles")]
        public async Task<ActionResult<IEnumerable<Style>>> GetLstStyle()
        {
            if (_dbContext.Styles.Count() > 0)
            {
                return await _dbContext.Styles.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api-get-list-Roles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetLstRoles()
        {
            if (_dbContext.Roles.Count() > 0)
            {
                return await _dbContext.Roles.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api-get-list-Nationals")]
        public async Task<ActionResult<IEnumerable<National>>> GetLstNationals()
        {
            if (_dbContext.Nationals.Count() > 0)
            {
                return await _dbContext.Nationals.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api-get-list-Colors")]
        public async Task<ActionResult<IEnumerable<Color>>> GetLstColors()
        {
            if (_dbContext.Colors.Count() > 0)
            {
                return await _dbContext.Colors.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api-get-list-DeliveryTypes")]
        public async Task<ActionResult<IEnumerable<DeliveryType>>> GetLstDeliveryTypes()
        {
            if (_dbContext.DeliveryTypes.Count() > 0)
            {
                return await _dbContext.DeliveryTypes.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api-get-list-Discounts")]
        public async Task<ActionResult<IEnumerable<Discount>>> GetLstDiscounts()
        {
            if (_dbContext.Discounts.Count() > 0)
            {
                return await _dbContext.Discounts.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api-get-list-Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetLstUsers()
        {
            if (_dbContext.Users.Count() > 0)
            {
                return await _dbContext.Users.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }
        //Query
        [HttpGet]
        [Route("api-get-user-byID")]
        public async Task<ActionResult<User>> GetUserByID(int id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            if (await _dbContext.Users.FindAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                return await _dbContext.Users.FindAsync(id);
            }
        }
        [HttpGet]
        [Route("api-get-user-byUserName")]
        public async Task<ActionResult<User>> GetUserByUserName(string username)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            if (_dbContext.Users.Where(c => c.Username == username).FirstOrDefaultAsync() == null)
            {
                return NotFound();
            }
            else
            {
                return await _dbContext.Users.Where(c => c.Username == username).FirstOrDefaultAsync();
            }
        }
        [HttpGet]
        [Route("api-get-user-byPhone")]
        public async Task<ActionResult<User>> GetUserByPhone(string phone)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            if (await _dbContext.Users.FirstOrDefaultAsync(c => c.Phone == phone) == null)
            {
                return NotFound();
            }
            else
            {
                return await _dbContext.Users.FirstOrDefaultAsync(c => c.Phone == phone);
            }
        }
        [HttpGet]
        [Route("api-get-user-byAddress")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByAddress(string address)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            if (await _dbContext.Users.Where(c => c.Address == address).ToListAsync() == null)
            {
                return NotFound();
            }
            else
            {
                return await _dbContext.Users.Where(c => c.Address == address).ToListAsync();
            }
        }
        [HttpGet]
        [Route("api-get-user-byStatus")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByStatus(string stt)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            if (await _dbContext.Users.Where(c => c.Status.ToString() == stt).ToListAsync() == null)
            {
                return NotFound();
            }
            else
            {
                return await _dbContext.Users.Where(c => c.Status.ToString() == stt).ToListAsync();
            }
        }
        [HttpGet]
        [Route("api-get-user-byRoleID")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByRoleID(string roleID)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            if (await _dbContext.Users.Where(c => c.RoleId.ToString() == roleID).ToListAsync() == null)
            {
                return NotFound();
            }
            else
            {
                return await _dbContext.Users.Where(c => c.RoleId.ToString() == roleID).ToListAsync();
            }
        }
        [HttpGet]
        [Route("api-get-user-byRoleName")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByRoleName(string rolename)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            if (await _dbContext.Users.Where(c => c.Role.RoleName == rolename).ToListAsync() == null)
            {
                return NotFound();
            }
            else
            {
                return await _dbContext.Users.Where(c => c.Role.RoleName == rolename).ToListAsync();
            }
        }
    }
}
