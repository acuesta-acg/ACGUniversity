using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UniversityWeb.Models;
using UniversityWeb.Serv;

namespace UniversityWeb.Controllers
{
    public class ExamenController : Controller
    {
        private readonly IServicioApi _sApi;

        public ExamenController(IServicioApi sApi)
        {
            _sApi = sApi;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SeguridadVM info)
        {
            _sApi.EnviarMsg(info.Texto);

            return View();
        }
    }
}
