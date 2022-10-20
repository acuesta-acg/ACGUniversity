using Acg.University.DAL.SqlServer;
using Acg.University.DAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Acg.University.BL.Contratos;

namespace Acg.University.BL.Servicios
{
    public class ServUsuarios : IServUsuarios
    {
        private UniversityDbContext _contexto;

        public ServUsuarios(UniversityDbContext contexto) => _contexto = contexto;

        public int NuevoUsuario(string login, string pwd) => NuevoUsuarioAsync(login, pwd).GetAwaiter().GetResult();
        
        public async Task<int> NuevoUsuarioAsync(string login, string pwd)
        {
            var usr = new Usuario() { Login = login, Passwd = pwd };

            try
            {
                await _contexto.Usuarios.AddAsync(usr);
                await _contexto.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }

            return usr.Id;
        }

        public void ModificarPwd(int id, string pwd) => ModificarPwdAsync(id, pwd).GetAwaiter().GetResult();

        public async Task ModificarPwdAsync(int id, string pwd)
        {
            var usr = await _contexto.Usuarios.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (usr != null)
            {
                usr.Passwd = pwd;

                _contexto.Usuarios.Update(usr);
                await _contexto.SaveChangesAsync();
            }
        }
        public int NuevoUsuario(string login, string pwd, string rol) =>
            NuevoUsuarioAsync(login, pwd, rol).GetAwaiter().GetResult();

        public async Task<int> NuevoUsuarioAsync(string login, string pwd, string rol)
        {
            var usr = await _contexto.Usuarios.Include("Roles").Where(x => x.Login == login).FirstOrDefaultAsync() ??
                new Usuario()
                {
                    Login = login,
                    Passwd = pwd,
                    Roles = new List<Rol>()
                };

            if (usr.Id > 0)
                foreach (var role in usr.Roles)
                    if (role.Nombre == rol)
                        return 0;

                var r = await _contexto.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync();


                usr.Roles.Add(r ?? new Rol() { Nombre = rol });

                try
                {
                    await _contexto.Usuarios.AddAsync(usr);
                    await _contexto.SaveChangesAsync();
                }
                catch
                {
                    return -1;
                }
            return usr.Id;
        }

        public Usuario? ConsultarUsuario(int id) =>
            ConsultarUsuarioAsync(id).GetAwaiter().GetResult();

        public async Task<Usuario?> ConsultarUsuarioAsync(int id) =>
            await _contexto.Usuarios.AsNoTracking()
            .Include("Roles").Where(u => u.Id == id).FirstOrDefaultAsync();

        public List<Usuario> ListaUsuarios() =>
            ListaUsuariosAsync().GetAwaiter().GetResult();

        public async Task<List<Usuario>> ListaUsuariosAsync() =>
            (await _contexto.Usuarios.AsNoTracking().Include("Roles").ToListAsync()).Select(x =>
            new Usuario() { Id = x.Id, Login = x.Login, Passwd ="De palo", Roles = x.Roles }).ToList();

        public Usuario? ConsultarUsuario(string login) => ConsultarUsuarioAsync(login).GetAwaiter().GetResult();

        public async Task<Usuario?> ConsultarUsuarioAsync(string login) =>
            await _contexto.Usuarios.Where(u => u.Login == login).FirstOrDefaultAsync();

        public Usuario? Login(string login, string pwd) => LoginAsync(login, pwd).GetAwaiter().GetResult();

        public async Task<Usuario?> LoginAsync(string login, string pwd) =>
            await _contexto.Usuarios.Include("Roles").Where(u => u.Login == login && u.Passwd == txt2txtHash(pwd)).FirstOrDefaultAsync();
        public Usuario? LoginMalo(string login, string pwd) => LoginMaloAsync(login, pwd).GetAwaiter().GetResult();

        public async Task<Usuario?> LoginMaloAsync(string login, string pwd) =>
            await _contexto.Usuarios
            .FromSqlRaw("SELECT * FROM Usuarios WHERE Login='" + login + "' AND Passwd='" + txt2txtHash(pwd) + "'")
            .Include("Roles")
            .FirstOrDefaultAsync();

        public void BorrarUsuario(int id) => BorrarUsuarioAsync(id).GetAwaiter().GetResult();
        public async Task BorrarUsuarioAsync(int id)
        {
            var u =  await _contexto.Usuarios.Include("Roles").Where(x => x.Id == id).FirstOrDefaultAsync();

            if (u!= null)
            {
                _contexto.Usuarios.Remove(u);
                await _contexto.SaveChangesAsync();
            }
        }
        public void BorrarUsuario(string login) => BorrarUsuarioAsync(login).GetAwaiter().GetResult();
        public async Task BorrarUsuarioAsync(string login)
        {
            var u = await _contexto.Usuarios.Include("Roles").Where(x => x.Login == login).FirstOrDefaultAsync();

            if (u != null)
            {
                _contexto.Usuarios.Remove(u);
                await _contexto.SaveChangesAsync();
            }
        }

        public void AsignarRolUsuario(int idUsr, string rol) => AsignarRolUsuarioAsync(idUsr, rol).GetAwaiter().GetResult();

        public async Task AsignarRolUsuarioAsync(int idUsr, string rol)
        {
            var u = await _contexto.Usuarios.Where(x => x.Id == idUsr).Include("Roles").FirstOrDefaultAsync();

            if (u != null)
            {
                var r = await _contexto.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync() ?? new Rol() { Nombre = rol};

                if (!u.Roles.Contains(r))
                {
                    u.Roles.Add(r);
                    _contexto.Usuarios.Update(u);
                    await _contexto.SaveChangesAsync();
                }
            }
        }

        public void AsignarRolUsuario(string login, string rol) => AsignarRolUsuarioAsync(login, rol).GetAwaiter().GetResult();

        public async Task AsignarRolUsuarioAsync(string login, string rol)
        {
            var u = await _contexto.Usuarios.Where(x => x.Login == login).Include("Roles").FirstOrDefaultAsync();

            if (u != null)
            {
                var r = await _contexto.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync() ?? new Rol() { Nombre = rol };

                if (!u.Roles.Contains(r))
                {
                    u.Roles.Add(r);
                    _contexto.Usuarios.Update(u);
                    await _contexto.SaveChangesAsync();
                }
            }
        }

        public void BorrarRolUsuario(int idUsr, string rol) => BorrarRolUsuarioAsync(idUsr, rol).GetAwaiter().GetResult();
        public async Task BorrarRolUsuarioAsync(int idUsr, string rol)
        {
            var u = await _contexto.Usuarios.Where(x => x.Id == idUsr).Include("Roles").FirstOrDefaultAsync();

            if (u != null)
            {
                var r = await _contexto.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync();
                if (r != null)
                {
                    if (!u.Roles.Contains(r))
                    {
                        u.Roles.Remove(r);
                        await _contexto.SaveChangesAsync();
                    }
                }
            }
        }
        public void BorrarRolUsuario(string login, string rol) => BorrarRolUsuarioAsync(login, rol).GetAwaiter().GetResult();
        public async Task BorrarRolUsuarioAsync(string login, string rol)
        {
            var u = await _contexto.Usuarios.Where(x => x.Login == login).Include("Roles").FirstOrDefaultAsync();
            if (u != null)
            {
                var r = await _contexto.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync();
                if (r != null)
                {
                    if (!u.Roles.Contains(r))
                    {
                        u.Roles.Remove(r);
                        await _contexto.SaveChangesAsync();
                    }
                }
            }
        }

        #region Parte de los roles
        public int NuevoRol(string nombre) => NuevoRolAsync(nombre).GetAwaiter().GetResult();

        public async Task<int> NuevoRolAsync(string nombre)
        {
            var rol = new Rol() { Nombre = nombre };

            try
            {
                await _contexto.Roles.AddAsync(rol);
                await _contexto.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }

            return rol.Id;
        }

        public Rol? ConsultarRol(int id) => ConsultarRolAsync(id).GetAwaiter().GetResult();
        public async Task<Rol?> ConsultarRolAsync(int id) =>
            await _contexto.Roles.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();

        public List<Rol> ListaRoles() => ListaRolesAsync().GetAwaiter().GetResult();
        public async Task<List<Rol>> ListaRolesAsync() =>
            await _contexto.Roles.AsNoTracking().ToListAsync();

        public void BorrarRol(int id) => BorrarRolAsync(id).GetAwaiter().GetResult();
        public async Task BorrarRolAsync(int id)
        {
            var r = await _contexto.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (r != null)
            {
                _contexto.Roles.Remove(r);
                await _contexto.SaveChangesAsync();
            }
        }

        public void BorrarRol(string rol) => BorrarRolAsync(rol).GetAwaiter().GetResult();
        public async Task BorrarRolAsync(string rol)
        {
            var r = await _contexto.Roles.Where(r => r.Nombre == rol).FirstOrDefaultAsync();
            if (r != null)
            {
                _contexto.Roles.Remove(r);
                await _contexto.SaveChangesAsync();
            }
        }

        #endregion

        public static string txt2txtHash(string texto)
        {
            using (SHA512 sHA = SHA512.Create())
            {
                var bs = sHA.ComputeHash(Encoding.UTF8.GetBytes(texto ?? String.Empty));

                StringBuilder sb = new StringBuilder();
                foreach (byte b in bs)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }
    }
}
