using Components.Consumers;
using Contracts;
using LoginMVC.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoginMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IRequestClient<ILoginRequest> _requestClient;
        private string _token;

        public HomeController(ILogger<HomeController> logger, IRequestClient<ILoginRequest> requestClient)
        {
            _logger = logger;
            _token = null;
            _requestClient = requestClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UsuarioLogado()
        {
            var mensagem = TempData["Mensagem"] as string;
            var model = new MessageModel { Message = mensagem };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var (status, notFound) = await _requestClient.GetResponse<ILoginResponse, ILoginNotFound>(new { Username = username, Password = password });
            
            if (status.IsCompletedSuccessfully)
            {
                var response = await status;
                TempData["Mensagem"] = response.Message.Message.ToString();
                return RedirectToAction("UsuarioLogado", "Home");
            }
            else
            {
                var response = await notFound;
                return BadRequest(response.Message);
            }
        } 

        private void SetToken()
        {
            _token = HttpContext.Session.GetString("username").ToString();
        }

    }
}