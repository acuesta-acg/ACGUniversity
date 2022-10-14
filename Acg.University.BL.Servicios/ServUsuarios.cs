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
        private UniversityDbContext _context;

        public ServUsuarios(UniversityDbContext context)
        {
            _context = context;
        }
        public int NuevoUsuario(string login, string pwd) => NuevoUsuarioAsync(login, pwd).GetAwaiter().GetResult();
        
        public async Task<int> NuevoUsuarioAsync(string login, string pwd)
        {
            var usr = new Usuario() { Login = login, Passwd = pwd };

            try
            {
                await _context.Usuarios.AddAsync(usr);
                await _context.SaveChangesAsync();
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
            var usr = await _context.Usuarios.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (usr != null)
            {
                usr.Passwd = pwd;

                _context.Usuarios.Update(usr);
                await _context.SaveChangesAsync();
            }
        }
        public int NuevoUsuario(string login, string pwd, string rol) =>
            NuevoUsuarioAsync(login, pwd, rol).GetAwaiter().GetResult();

        public async Task<int> NuevoUsuarioAsync(string login, string pwd, string rol)
        {
            var usr = await _context.Usuarios.Include("Roles").Where(x => x.Login == login).FirstOrDefaultAsync() ??
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

                var r = await _context.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync();


                usr.Roles.Add(r ?? new Rol() { Nombre = rol });

                try
                {
                    await _context.Usuarios.AddAsync(usr);
                    await _context.SaveChangesAsync();
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
            await _context.Usuarios.AsNoTracking().Include("Roles").Where(u => u.Id == id).FirstOrDefaultAsync();

        public List<Usuario> ListaUsuarios() =>
            ListaUsuariosAsync().GetAwaiter().GetResult();

        public async Task<List<Usuario>> ListaUsuariosAsync() =>
            (await _context.Usuarios.AsNoTracking().Include("Roles").ToListAsync()).Select(x =>
            new Usuario() { Id = x.Id, Login = x.Login, Passwd ="De palo", Roles = x.Roles }).ToList();
      
        public Usuario? ConsultarUsuario(string login) => _context.Usuarios.Where(u => u.Login == login).FirstOrDefault();

        public async Task<Usuario?> ConsultarUsuarioAsync(string login) =>
            await _context.Usuarios.Where(u => u.Login == login).FirstOrDefaultAsync();

        public Usuario? Login(string login, string pwd) =>
            _context.Usuarios.Where(u => u.Login == login && u.Passwd == pwd).FirstOrDefault();

        public async Task<Usuario?> LoginAsync(string login, string pwd) =>
            await _context.Usuarios.Where(u => u.Login == login && u.Passwd == pwd).FirstOrDefaultAsync();

        public void BorrarUsuario(int id) => BorrarUsuarioAsync(id).GetAwaiter().GetResult();
        public async Task BorrarUsuarioAsync(int id)
        {
            var u =  await _context.Usuarios.Include("Roles").Where(x => x.Id == id).FirstOrDefaultAsync();

            if (u!= null)
            {
                _context.Usuarios.Remove(u);
                await _context.SaveChangesAsync();
            }
        }
        public void BorrarUsuario(string login) => BorrarUsuarioAsync(login).GetAwaiter().GetResult();
        public async Task BorrarUsuarioAsync(string login)
        {
            var u = await _context.Usuarios.Include("Roles").Where(x => x.Login == login).FirstOrDefaultAsync();

            if (u != null)
            {
                _context.Usuarios.Remove(u);
                await _context.SaveChangesAsync();
            }
        }

        public void AsignarRolUsuario(int idUsr, string rol) => AsignarRolUsuarioAsync(idUsr, rol).GetAwaiter().GetResult();

        public async Task AsignarRolUsuarioAsync(int idUsr, string rol)
        {
            var u = await _context.Usuarios.Where(x => x.Id == idUsr).Include("Roles").FirstOrDefaultAsync();

            if (u != null)
            {
                var r = await _context.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync() ?? new Rol() { Nombre = rol};

                if (!u.Roles.Contains(r))
                {
                    u.Roles.Add(r);
                    _context.Usuarios.Update(u);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public void AsignarRolUsuario(string login, string rol) => AsignarRolUsuarioAsync(login, rol).GetAwaiter().GetResult();

        public async Task AsignarRolUsuarioAsync(string login, string rol)
        {
            var u = await _context.Usuarios.Where(x => x.Login == login).Include("Roles").FirstOrDefaultAsync();

            if (u != null)
            {
                var r = await _context.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync() ?? new Rol() { Nombre = rol };

                if (!u.Roles.Contains(r))
                {
                    u.Roles.Add(r);
                    _context.Usuarios.Update(u);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public void BorrarRolUsuario(int idUsr, string rol) => BorrarRolUsuarioAsync(idUsr, rol).GetAwaiter().GetResult();
        public async Task BorrarRolUsuarioAsync(int idUsr, string rol)
        {
            var u = await _context.Usuarios.Where(x => x.Id == idUsr).Include("Roles").FirstOrDefaultAsync();

            if (u != null)
            {
                var r = await _context.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync();
                if (r != null)
                {
                    if (!u.Roles.Contains(r))
                    {
                        u.Roles.Remove(r);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
        public void BorrarRolUsuario(string login, string rol) => BorrarRolUsuarioAsync(login, rol).GetAwaiter().GetResult();
        public async Task BorrarRolUsuarioAsync(string login, string rol)
        {
            var u = await _context.Usuarios.Where(x => x.Login == login).Include("Roles").FirstOrDefaultAsync();
            if (u != null)
            {
                var r = await _context.Roles.Where(x => x.Nombre == rol).FirstOrDefaultAsync();
                if (r != null)
                {
                    if (!u.Roles.Contains(r))
                    {
                        u.Roles.Remove(r);
                        await _context.SaveChangesAsync();
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
                await _context.Roles.AddAsync(rol);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }

            return rol.Id;
        }

        public Rol? ConsultarRol(int id) => ConsultarRolAsync(id).GetAwaiter().GetResult();
        public async Task<Rol?> ConsultarRolAsync(int id) =>
            await _context.Roles.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();

        public List<Rol> ListaRoles() => ListaRolesAsync().GetAwaiter().GetResult();
        public async Task<List<Rol>> ListaRolesAsync() =>
            await _context.Roles.AsNoTracking().ToListAsync();

        public void BorrarRol(int id) => BorrarRolAsync(id).GetAwaiter().GetResult();
        public async Task BorrarRolAsync(int id)
        {
            var r = await _context.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (r != null)
            {
                _context.Roles.Remove(r);
                await _context.SaveChangesAsync();
            }
        }

        public void BorrarRol(string rol) => BorrarRolAsync(rol).GetAwaiter().GetResult();
        public async Task BorrarRolAsync(string rol)
        {
            var r = await _context.Roles.Where(r => r.Nombre == rol).FirstOrDefaultAsync();
            if (r != null)
            {
                _context.Roles.Remove(r);
                await _context.SaveChangesAsync();
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
