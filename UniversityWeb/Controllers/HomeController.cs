using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniversityWeb.Models;
using UniversityWeb.Serv;

namespace UniversityWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServicioApi _servApi;
        private IDataProtector _dataProtector;

        public HomeController(ILogger<HomeController> logger, IServicioApi servApi, IDataProtectionProvider prov)
        {
            _logger = logger;
            _servApi = servApi;
            _dataProtector = prov.CreateProtector("Hola Soy Coco...");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            var m = new InfoAdministradoresVM()
            {
                Administradores = await _servApi.ConsultarAdministradoresAsync()
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Privacy(InfoAdministradoresVM info)
        {
            return View(info);
        }

        [HttpGet]
        public async Task<IActionResult> Seguridad()
        {
            return View(new SeguridadVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Seguridad(SeguridadVM info)
        {
            return View(info);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}