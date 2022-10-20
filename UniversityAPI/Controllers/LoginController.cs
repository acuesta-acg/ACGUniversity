using Acg.University.BL.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IServUsuarios _sUsuarios;
        private readonly IConfiguration _conf;
        public LoginController(IConfiguration conf, IServUsuarios sUsuarios)
        {
            _sUsuarios = sUsuarios;
            _conf = conf;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credenciales cred)
        {
            var usr = await _sUsuarios.LoginMaloAsync(cred.login, cred.pwd);
            if (usr != null)
            {
                var c = new List<Claim> {
                        new Claim(ClaimTypes.Name, usr.Login),
                        new Claim(ClaimTypes.NameIdentifier, usr.Id.ToString())
                    };
                foreach (var r in usr.Roles)
                    c.Add(new Claim(ClaimTypes.Role, r.Nombre));

                var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["jwt:Key"]));
                var crd = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);

                var tokenDesc = new JwtSecurityToken(
                    issuer: _conf["jwt:Issuer"],
                    audience: _conf["jwt:Audience"],
                    claims: c,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(120),
                    signingCredentials: crd);

                var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDesc);

                return Ok(jwt);
            }
            return BadRequest();
        }
    }

    public class Credenciales
    {
        public string login { get; set; }
        public string pwd { get; set; }
    }
}
