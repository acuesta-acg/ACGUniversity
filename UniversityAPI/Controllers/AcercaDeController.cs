using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using ZstdSharp.Unsafe;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcercaDeController : ControllerBase
    {
        private readonly IStringLocalizer<AcercaDeController> _stringLocalizer;
        private readonly IStringLocalizer<RecursosGenerales> _stringLocalizer2;

        public AcercaDeController(
            IStringLocalizer<AcercaDeController> stringLocalizer,
            IStringLocalizer<RecursosGenerales> stringLocalizer2)
        {
            _stringLocalizer = stringLocalizer;
            _stringLocalizer2 = stringLocalizer2;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //return Ok(_stringLocalizer.GetString("Saludo").Value ?? "");
            return Ok(_stringLocalizer2.GetString("Saludo").Value ?? "");
        }
    }
}
