using Acg.University.BL.Contratos;
using Acg.University.DAL.Entidades;
using Acg.University.DAL.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.BL.Servicios
{
    public class ServPersonas: IServPersonas
    {
        private UniversityDbContext _contexto;

        public ServPersonas(UniversityDbContext contexto) => _contexto = contexto;

        #region Gestión de personas
        public int NuevaPersona(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd,
            string rol) => NuevaPersonaAsync(dni, nombre, direc, poblac, prov, cp, mail, tel, login, pwd, rol)
            .GetAwaiter().GetResult();
        public async Task<int> NuevaPersonaAsync(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd,
            string rol)
        {
            var r = await _contexto.Roles.Where(r => r.Nombre == rol).FirstOrDefaultAsync() ?? new Rol() { Nombre = rol };

            var u = await _contexto.Usuarios.Include("Roles").Where(u => u.Login == login).FirstOrDefaultAsync() ??
                new Usuario() { Login = login, Passwd = pwd, Roles = new List<Rol>() };

            if (!u.Roles.Contains(r))
                u.Roles.Add(r);

            Persona persona = new Persona()
            {
                DNI = dni,
                Nombre = nombre,
                Direccion = direc,
                Poblacion = poblac,
                Provincia = prov,
                CodPostal = cp,
                EMail = mail,
                Telefonos = new List<TelefonoPersona>() { new TelefonoPersona() { Numero = tel } },
                Usuario = u
            };

            await _contexto.Personas.AddAsync(persona);

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }

            return persona.Id;
        }

        public int ModificarPersona(
            int id,
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel) => ModificarPersonaAsync(
                id,
                dni,
                nombre,
                direc,
                poblac,
                prov,
                cp,
                mail,
                tel).GetAwaiter().GetResult();

        public async Task<int> ModificarPersonaAsync(
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
            Persona? persona = await _contexto.Personas
                .Include("Telefonos")
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (persona != null)
            {
                persona.DNI = dni;
                persona.Nombre = nombre;
                persona.Direccion = direc;
                persona.Poblacion = poblac;
                persona.Provincia = prov;
                persona.CodPostal = cp;
                persona.EMail = mail;
                persona.Telefonos[0].Numero = tel;

                _contexto.Personas.Update(persona);

                try
                {
                    await _contexto.SaveChangesAsync();
                }
                catch
                {
                    return -1;
                }
            }

            return 0;
        }

        public List<Persona> ListaPersonas() => ListaPersonasAsync().GetAwaiter().GetResult();
        public async Task<List<Persona>> ListaPersonasAsync() =>
            await _contexto.Personas.AsNoTracking()
            .Include("Telefonos")
            .Include("Usuario")
            .Include("Usuario.Roles")
            .ToListAsync();

        public Persona? ConsultarPersona(int id) => ConsultarPersonaAsync(id).GetAwaiter().GetResult();
        public async Task<Persona?> ConsultarPersonaAsync(int id) =>
            await _contexto.Personas
            .Include("Telefonos")
            .Include("Usuario")
            .Include("Usuario.Roles")
            .Where(x => x.Id == id).FirstOrDefaultAsync();

        public Persona? ConsultarPersona(string dni) => ConsultarPersonaAsync(dni).GetAwaiter().GetResult();
        public async Task<Persona?> ConsultarPersonaAsync(string dni) =>
            await _contexto.Personas
            .Include("Telefonos")
            .Include("Usuario")
            .Include("Usuario.Roles")
            .Where(x => x.DNI == dni).FirstOrDefaultAsync();

        public void BorrarPersona(int id) => BorrarPersonaAsync(id).GetAwaiter().GetResult();

        public async Task BorrarPersonaAsync(int id)
        {
            var p = await _contexto.Personas
             .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (p != null)
            {
                _contexto.Personas.Remove(p);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<List<CuantosTelPersona>> ListaVistaEjemplo() => await _contexto.CuantosTelefonos.ToListAsync();

        #endregion

        #region Gestión de profesores

        public int NuevoProfesor(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd) => NuevoAdministradorAsync(
                dni,
                nombre,
                direc,
                poblac,
                prov,
                cp,
                mail,
                tel,
                login,
                pwd).GetAwaiter().GetResult();

        public async Task<int> NuevoProfesorAsync(
            string dni,
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
            var r = await _contexto.Roles.Where(r => r.Nombre == "Profesor").FirstOrDefaultAsync() ??
                new Rol() { Nombre = "Profesor" };

            var p = (await _contexto.Personas.Include("Usuario")
                .Include("Persona.Telefonos")
                .Include("Usuario.Roles")
                .Where(x => x.DNI == dni).FirstOrDefaultAsync()) ??
                new Persona()
                {
                    DNI = dni,
                    Nombre = nombre,
                    Direccion = direc,
                    Poblacion = poblac,
                    Provincia = prov,
                    CodPostal = cp,
                    EMail = mail,
                    Telefonos = new List<TelefonoPersona>() { new TelefonoPersona() { Numero = tel } },
                    Usuario = await _contexto.Usuarios.Include("Roles").Where(x => x.Login == login).FirstOrDefaultAsync() ??
                    new Usuario()
                    {
                        Login = login,
                        Passwd = ServUsuarios.txt2txtHash(pwd),
                        Roles = new List<Rol>() { r }
                    },
                };

            if (!p.Usuario.Roles.Contains(r))
                p.Usuario.Roles.Add(r);

            var profesor = new Profesor() { Persona = p };

            await _contexto.Profesores.AddAsync(profesor);
            await _contexto.SaveChangesAsync();

            return profesor.Id;
        }

        public Profesor? ConsultarProfesor(int id) => ConsultarProfesorAsync(id).GetAwaiter().GetResult();
        public async Task<Profesor?> ConsultarProfesorAsync(int id) =>
            await _contexto.Profesores.AsNoTracking().Include("Persona").Where(p => p.Id == id).FirstOrDefaultAsync();

        public Profesor? ConsultarProfesor(string dni) => ConsultarProfesorAsync(dni).GetAwaiter().GetResult();
        public async Task<Profesor?> ConsultarProfesorAsync(string dni) =>
            await _contexto.Profesores.AsNoTracking().Include("Persona").Where(p => p.Persona.DNI == dni).FirstOrDefaultAsync();
        public List<Profesor> ListaProfesores() => ListaProfesoresAsync().GetAwaiter().GetResult();
        public async Task<List<Profesor>> ListaProfesoresAsync() =>
            await _contexto.Profesores.AsNoTracking().Include("Persona").ToListAsync();
        public void BorrarProfesor(int id) => BorrarProfesorAsync(id).GetAwaiter().GetResult();
        public async Task BorrarProfesorAsync(int id)
        {
            var r = _contexto.Roles.Where(x => x.Nombre == "Profesor").FirstOrDefault();

            var p = await _contexto.Profesores
                .Include("Persona")
                .Include("Persona.Usuario")
                .Include("Persona.Usuario.Roles")
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (p != null)
            {
                if (r != null)
                    p.Persona.Usuario?.Roles.Remove(r);

                _contexto.Profesores.Remove(p);
                await _contexto.SaveChangesAsync();
            }
        }

        #endregion

        #region Gestión de alumnos

        public int NuevoAlumno(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd) => NuevoAlumnoAsync(
                dni,
                nombre,
                direc,
                poblac,
                prov,
                cp,
                mail,
                tel,
                login,
                pwd).GetAwaiter().GetResult();

        public async Task<int> NuevoAlumnoAsync(
            string dni,
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
            var r = await _contexto.Roles.Where(r => r.Nombre == "Alumno").FirstOrDefaultAsync() ??
                new Rol() { Nombre = "Alumno" };

            var p = (await _contexto.Personas.Include("Usuario")
                .Include("Persona.Telefonos")
                .Include("Usuario.Roles")
                .Where(x => x.DNI == dni).FirstOrDefaultAsync()) ??
                new Persona()
                {
                    DNI = dni,
                    Nombre = nombre,
                    Direccion = direc,
                    Poblacion = poblac,
                    Provincia = prov,
                    CodPostal = cp,
                    EMail = mail,
                    Telefonos = new List<TelefonoPersona>() { new TelefonoPersona() { Numero = tel } },
                    Usuario = _contexto.Usuarios.Include("Roles").Where(x => x.Login == login).FirstOrDefault() ??
                    new Usuario()
                    {
                        Login = login,
                        Passwd = ServUsuarios.txt2txtHash(pwd),
                        Roles = new List<Rol>()
                    },
                };

            if (!p.Usuario.Roles.Contains(r))
                p.Usuario.Roles.Add(r);
            else
                return -1;

            var alumno = new Alumno() { Persona = p };

            await _contexto.Alumnos.AddAsync(alumno);
            await _contexto.SaveChangesAsync();

            return alumno.Id;
        }

        public Alumno? ConsultarAlumno(int id) => ConsultarAlumnoAsync(id).GetAwaiter().GetResult();
        public async Task<Alumno?> ConsultarAlumnoAsync(int id) =>
            await _contexto.Alumnos.AsNoTracking()
            .Include("Persona")
            .Include("Persona.Telefonos")
            .Where(p => p.Id == id).FirstOrDefaultAsync();

        public Alumno? ConsultarAlumno(string dni) => ConsultarAlumnoAsync(dni).GetAwaiter().GetResult();
        public async Task<Alumno?> ConsultarAlumnoAsync(string dni) =>
            await _contexto.Alumnos.AsNoTracking()
            .Include("Persona")
            .Include("Persona.Telefonos")
            .Where(p => p.Persona.DNI == dni).FirstOrDefaultAsync();
        public List<Alumno> ListaAlumnos() => ListaAlumnosAsync().GetAwaiter().GetResult();
        public async Task<List<Alumno>> ListaAlumnosAsync() =>
            await _contexto.Alumnos.AsNoTracking().Include("Persona").ToListAsync();
        public void BorrarAlumno(int id) => BorrarAlumnoAsync(id).GetAwaiter().GetResult();
        public async Task BorrarAlumnoAsync(int id)
        {
            var r = _contexto.Roles.Where(x => x.Nombre == "Alumno").FirstOrDefault();

            var a = await _contexto.Alumnos
                .Include("Persona")
                .Include("Persona.Usuario")
                .Include("Persona.Usuario.Roles")
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (a != null)
            {
                if (r != null)
                    a.Persona?.Usuario?.Roles.Remove(r);

                _contexto.Alumnos.Remove(a);
                await _contexto.SaveChangesAsync();
            }
        }

        #endregion

        #region Gestión de administradores

        public int NuevoAdministrador(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd) => NuevoAdministradorAsync(dni,
                nombre,
                direc,
                poblac,
                prov,
                cp,
                mail,
                tel,
                login,
                pwd).GetAwaiter().GetResult();

        public async Task<int> NuevoAdministradorAsync(
            string dni,
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
            var r = _contexto.Roles.Where(x => x.Nombre == "Admin").FirstOrDefault() ??
                        new Rol() { Nombre = "Admin" };

            var p = (await _contexto.Personas.Include("Usuario").Include("Usuario.Roles")
                .Where(x => x.DNI == dni).FirstOrDefaultAsync()) ??
                new Persona()
                {
                    DNI = dni,
                    Nombre = nombre,
                    Direccion = direc,
                    Poblacion = poblac,
                    Provincia = prov,
                    CodPostal = cp,
                    EMail = mail,
                    Telefonos = new List<TelefonoPersona>() { new TelefonoPersona() { Numero = tel } },
                    Usuario = _contexto.Usuarios.Include("Roles").Where(x => x.Login == login).FirstOrDefault() ??
                    new Usuario()
                    {
                        Login = login,
                        Passwd = ServUsuarios.txt2txtHash(pwd),
                        Roles = new List<Rol>()
                    },
                };

            if (!p.Usuario.Roles.Contains(r))
                p.Usuario.Roles.Add(r);
            else
                return -1;

            var adm = new Administrador() { Persona = p };

            await _contexto.Administradores.AddAsync(new Administrador() { Persona = p });
            await _contexto.SaveChangesAsync();

            return adm.Id;
        }

        public int ModificarAdministrador(
            int id,
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel) => ModificarAdministradorAsync(
                id,
                dni,
                nombre,
                direc,
                poblac,
                prov,
                cp,
                mail,
                tel).GetAwaiter().GetResult();

        public async Task<int> ModificarAdministradorAsync(
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
            Administrador? adm = await _contexto.Administradores
                .Include("Persona")
                .Include("Persona.Telefonos")
                .Where(a => a.Id == id).FirstOrDefaultAsync();

            if (adm != null)
            {
                adm.Persona.DNI = dni;
                adm.Persona.Nombre = nombre;
                adm.Persona.Direccion = direc;
                adm.Persona.Poblacion = poblac;
                adm.Persona.Provincia = prov;
                adm.Persona.CodPostal = cp;
                adm.Persona.EMail = mail;
                adm.Persona.Telefonos[0].Numero = tel;

                _contexto.Administradores.Update(adm);

                try
                {
                    await _contexto.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }

            return 0;
        }
        public Administrador? ConsultarAdministrador(int id) => ConsultarAdministradorAsync(id).GetAwaiter().GetResult();
        public async Task<Administrador?> ConsultarAdministradorAsync(int id) =>
            await _contexto.Administradores.AsNoTracking()
            .Include("Persona")
            .Include("Persona.Telefonos")
            .Where(p => p.Id == id).FirstOrDefaultAsync();

        public Administrador? ConsultarAdministrador(string dni) => ConsultarAdministradorAsync(dni).GetAwaiter().GetResult();
        public async Task<Administrador?> ConsultarAdministradorAsync(string dni) =>
            await _contexto.Administradores.AsNoTracking()
            .Include("Persona")
            .Include("Persona.Telefonos")
            .Where(p => p.Persona.DNI == dni).FirstOrDefaultAsync();
        public List<Administrador> ListaAdministradores() => ListaAdministradoresAsync().GetAwaiter().GetResult();
        public async Task<List<Administrador>> ListaAdministradoresAsync() =>
            await _contexto.Administradores.AsNoTracking()
            .Include("Persona")
            .Include("Persona.Telefonos")
            .Include("Persona.Usuario")
            .Include("Persona.Usuario.Roles")
            .ToListAsync();
        public void BorrarAdministrador(int id) => BorrarAdministradorAsync(id).GetAwaiter().GetResult();
        public async Task BorrarAdministradorAsync(int id)
        {
            var r = _contexto.Roles.Where(x => x.Nombre == "Admin").FirstOrDefault();

            var a = await _contexto.Administradores
                .Include("Persona")
                .Include("Persona.Telefonos")
                .Include("Persona.Usuario")
                .Include("Persona.Usuario.Roles")
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (a != null)
            {
                if (r != null)
                    a.Persona.Usuario.Roles.Remove(r);

                _contexto.Administradores.Remove(a);
                await _contexto.SaveChangesAsync();
            }
        }

        #endregion
    }
}
