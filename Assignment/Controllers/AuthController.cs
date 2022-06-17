using Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Assignment.Controllers
{
    public class AuthController : Controller
    {

        private readonly C5_AssignmentContext _assignmentContext;

        public AuthController(C5_AssignmentContext context)
        {
            _assignmentContext = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var c5_AssignmentContext = _assignmentContext.Users.Include(u => u.Role);
            return View(await c5_AssignmentContext.ToListAsync());
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            //Khai báo 1 httpClient để gọi api
            HttpClient client = new HttpClient();
            //set đường dẫn cơ bản  gọi đến api/user1
           string baseAddress =  "https://localhost:7110/api/auths";

           var user =  await client.GetFromJsonAsync<User>(baseAddress + "/getusername?username=" + login.Username);
            if(user==null)
            {
                ModelState.AddModelError(String.Empty, "Tài khoản không tồn tại");
            }
            else
            {
                if(user.Password != login.Password)
                {
                    ModelState.AddModelError(String.Empty, "Sai mật khẩu");
                }
                else
                {
                    HttpContext.Session.SetString("username", user.Username);
                    HttpContext.Session.SetString("roleId", user.RoleId.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(login);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Edit/5
        // GET: Discountsmvc/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Discountsmvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var jsondata = client.PostAsJsonAsync("api/Auths/post-user", user).Result;
                var check = jsondata.IsSuccessStatusCode;
                return RedirectToAction("Login", "Auth");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordVM userForgot)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var result = await client.PostAsJsonAsync("api/Auths/check-phone", userForgot);
                if (result.IsSuccessStatusCode)
                {
                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError(string.Empty, "Phone is not found");
                    }
                    if(result.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction("ChangePassword", "Auth", JsonConvert.DeserializeObject<User>(await result.Content.ReadAsStringAsync()));
                    }
                }
                ModelState.AddModelError(string.Empty, "Not responsed");
            }
            return View(userForgot);
        }

        [HttpGet]
        public IActionResult ChangePassword(User user)
        {
            var userChange = new UserChangePasswordVM() { UserName = user.Username, Phone = user.Phone };
            return View(userChange);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordVM changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:7110/");
                var result = await client.PostAsJsonAsync("api/Auths/change-password", changePasswordVM);
                if (result.IsSuccessStatusCode)
                {
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                }
                ModelState.AddModelError(string.Empty, "Not responsed");
            }
            return View(changePasswordVM);
        }
    }
       
}
