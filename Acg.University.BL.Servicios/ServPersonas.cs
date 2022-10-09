using Acg.University.DAL.Entidades;
using Acg.University.DAL.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.BL.Servicios
{
    public class ServPersonas
    {
        private UniversityDbContext _contexto;

        public ServPersonas(UniversityDbContext contexto) => _contexto = contexto;

        /*
         * public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion {  get; set; }
        public string Poblacion { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        public Usuario Usuario { get; set; }
         */
        public async Task<int> NuevoAsync(string nombre, string direcc, string poblac, string prov, string tel)
        {
            var p = new Persona()
            {
                Nombre = nombre,
                Direccion = direcc,
                Poblacion = poblac,
                Provincia = prov,
                Telefonos = new List<TelefonoPersona>() { new TelefonoPersona() { Numero = tel }, new TelefonoPersona() { Numero = "asdfasdf" } }
            };

            try
            {
                await _contexto.Personas.AddAsync(p);
                await _contexto.SaveChangesAsync();
            } catch
            {
                return -1;
            }

            return p.Id;
        }

        public async Task<int> NuevoAsync(string nombre, string direcc, string poblac, string prov, string tel, string login, string pwd, string rol)
        {
            var rl = _contexto.Roles.Where(x => x.Nombre == rol).FirstOrDefault() ?? new Rol() { Nombre = rol };

            var lg = _contexto.Usuarios.Include("Roles").Where(x => x.Login == login).FirstOrDefault();

            var p = new Persona()
            {
                Nombre = nombre,
                Direccion = direcc,
                Poblacion = poblac,
                Provincia = prov,
                Telefonos = new List<TelefonoPersona>() { new TelefonoPersona() { Numero = tel }, new TelefonoPersona() { Numero = "asdfasdf" } },
                Usuario = lg ?? new Usuario() { Login =login, Passwd = pwd, Roles = new List<Rol>() { rl } }
            };

            try
            {
                await _contexto.Personas.AddAsync(p);
                await _contexto.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }

            return p.Id;
        }

        public async Task<List<Persona>> Lista() =>
            await _contexto.Personas.AsNoTracking()
            .Include("Telefonos")
            .Include("Usuario")
            .Include("Usuario.Roles")
            .ToListAsync();
        public async Task<Persona?> Consultar(int id) =>
            await _contexto.Personas.Include("Usuario")
            .Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<CuantosTelPersona>> ListaVistaEjemplo() => await _contexto.CuantosTelefonos.ToListAsync();
    }
}
