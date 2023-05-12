using GetInfoLoginMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GetInfoLoginMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string _token;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _token = null;
        }

        public IActionResult Index()
        {
            if (_token is not null)
            {
                ViewBag.User = _token;
            }
            else
            {
                ViewBag.User = "Deu erro né";
            }

            return View();
        }

        public async Task PostUsername(HttpContent content)
        {
            //HttpClient client = new HttpClient();
            //_token = await client.GetAsync("http://localhost:5551/Home/GetToken");
        }
    }
}