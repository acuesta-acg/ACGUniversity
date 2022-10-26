using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using UniversityWeb.Models;
using UniversityWeb.Serv;

namespace UniversityWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicioApi _servApi;

        public LoginController(IServicioApi servApi)
        {
            _servApi = servApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> LogIn(LoginViewModel? modelo = null)
        {
            if (modelo != null)
            {
                var cp = await _servApi.Login(modelo.Usuario, modelo.Password);
                if (cp != null)
                {
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        cp,
                        new AuthenticationProperties { });
                    return RedirectToAction("Index", "Chat");
                }
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
