using Acg.University.BL.Contratos;
using Acg.University.DAL.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.RegularExpressions;
using UniversityAPI.Views;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    //[Authorize(Roles = "Admin")]
    public class AdministradoresController : ControllerBase
    {
        private readonly IServPersonas _servPersonas;

        public AdministradoresController(IServPersonas servPersonas)
        {
            _servPersonas = servPersonas;
        }

        /*
         * GET
         * POST
         * PUT
         * DELETE
         * PATCH
         */

        /*
         * Objeto
         * IActionResult
         * ActionResult
         */

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Lista()
        {
            var l =(await _servPersonas.ListaAdministradoresAsync())
               .Select(x => new AdministradorVM()
               {
                   id = x.Id,
                   idPersona = x.PersonaId,
                   dni = x.Persona.DNI,
                   direc = x.Persona.Direccion,
                   nombre = x.Persona.Nombre,
                   poblac = x.Persona.Poblacion,
                   prov = x.Persona.Provincia
               }).ToList();

            return  (l.Count== 0) ? NotFound() : Ok(l);
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd)
        {
            await _servPersonas.NuevoAdministradorAsync(
                dni,
                nombre,
                direc,
                poblac,
                prov,
                cp,
                mail,
                tel,
                login,
                pwd);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Modificar(
            int id,
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel)
        {
            await _servPersonas.ModificarPersonaAsync(
                id,
                dni,
                nombre,
                direc,
                poblac,
                prov,
                cp,
                mail,
                tel);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Borrar(int id)
        {
            await _servPersonas.BorrarAdministradorAsync(id);

            return Ok();
        }
    }

    [Route("api/Administradores")]
    [ApiController]
    [ApiVersion("2.0")]
    public class Administradores2Controller : ControllerBase
    {
        private readonly IServPersonas _servPersonas;

        public Administradores2Controller(IServPersonas servPersonas)
        {
            _servPersonas = servPersonas;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var l = (await _servPersonas.ListaAdministradoresAsync())
               .Select(x => new Administrador2VM()
               {
                   id = x.Id,
                   idPersona = x.PersonaId,
                   dni = x.Persona.DNI,
                   direc = x.Persona.Direccion,
                   nombre = x.Persona.Nombre,
                   poblac = x.Persona.Poblacion,
                   prov = x.Persona.Provincia,
                   tel = "un teléfono",
                   cp = x.Persona.CodPostal
               }).ToList();

            return (l.Count == 0) ? NotFound() : Ok(l);
        }
    }
}
