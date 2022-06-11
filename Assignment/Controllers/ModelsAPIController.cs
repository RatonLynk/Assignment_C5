using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment.Model;
using Microsoft.EntityFrameworkCore;
namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsAPIController : ControllerBase
    {
        List<QLSP> _lstQLSP;

        C5_AssignmentContext _dbContext = new C5_AssignmentContext();
        public ModelsAPIController(C5_AssignmentContext dbContext)
        {

            _dbContext = dbContext;
            _lstQLSP=new List<QLSP>();
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
        [HttpGet]
        [Route("api-get-all")]
        public async Task<IEnumerable<QLSP>> GetAll()
        {
            _lstQLSP = (from a in _dbContext.Products.ToList()
                        join b in _dbContext.ProductDetails.ToList() on a.ProductId equals b.ProductDetailId
                        join c in _dbContext.ColorProducts.ToList() on a.ProductId equals c.ProductId
                        join d in _dbContext.Colors.ToList() on c.ColorId equals d.ColorId
                        join e in _dbContext.Brands.ToList() on b.BrandId equals e.BrandId
                        join f in _dbContext.Styles.ToList() on b.StyleId equals f.StyleId
                        join g in _dbContext.Nationals.ToList() on b.NationalId equals g.NationalId
                        join h in _dbContext.Categories.ToList() on b.CategoryId equals h.CategoryId
                        join i in _dbContext.DiscountProducts.ToList() on b.ProductDetailId equals i.ProductId
                        join j in _dbContext.Discounts.ToList() on i.DiscountId equals j.DiscountId
                        join k in _dbContext.Images.ToList() on b.ProductDetailId equals k.ProductId
                        select new QLSP
                        {
                            Product = a,
                            ProductDetail = b,
                            ColorProduct = c,
                            Color = d,
                            Brand = e,
                            Style = f,
                            National = g,
                            Category = h,
                            DiscountProduct = i,
                            Discount = j,
                            Image = k
                        }
                      ).ToList();
            return _lstQLSP;
        }
    }
}
