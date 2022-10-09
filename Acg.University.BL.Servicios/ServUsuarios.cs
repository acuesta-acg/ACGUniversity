using Acg.University.DAL.SqlServer;
using Acg.University.DAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Acg.University.BL.Servicios
{
    public class ServUsuarios
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
            var usr = _context.Usuarios.Where(u => u.Id == id).FirstOrDefault();

            if (usr != null)
            {
                usr.Passwd = pwd;

                _context.Usuarios.Update(usr);
                _context.SaveChanges();
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

        public List<Usuario>? ListaUsuario() =>
            ListaUsuariosAsync().GetAwaiter().GetResult();

        public async Task<List<Usuario>?> ListaUsuariosAsync() =>
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

        public async Task<Rol?> ConsultarRolAsync(int id) =>
            await _context.Roles.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();

        #endregion
    }
}
