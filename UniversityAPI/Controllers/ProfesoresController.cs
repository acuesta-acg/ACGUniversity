using Acg.University.BL.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Views;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly IServPersonas _servPersonas;

        public ProfesoresController(IServPersonas servPersonas)
        {
            _servPersonas = servPersonas;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var l = (await _servPersonas.ListaProfesoresAsync())
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

            return (l.Count == 0) ? NotFound() : Ok(l);
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
            await _servPersonas.NuevoProfesorAsync(
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
        public async Task<IActionResult> Borrar(int id)
        {
            await _servPersonas.BorrarAlumnoAsync(id);

            return Ok();
        }
    }
}
