using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7110/api/ModelsAPI/");
            var JsonConnect = client.GetAsync("ok").Result;
            string path = "https://localhost:7110/api/ModelsAPI/ok";
            
            string JsonData = JsonConnect.Content.ReadAsStringAsync().Result;

            //JObject jObject=JObject.Parse(productDetail.ToString());
            ViewData["data"] = JsonData;

            var model = JsonConvert.DeserializeObject<List<ViewSanPham>>(JsonData);
            //ViewBag.data = jObject["results"];
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}