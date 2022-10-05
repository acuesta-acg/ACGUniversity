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

        public int NuevoUsuario(string login, string pwd, string rol) =>
            NuevoUsuarioAsync(login, pwd, rol).GetAwaiter().GetResult();

        public async Task<int> NuevoUsuarioAsync(string login, string pwd, string rol)
        {
            var usr = new Usuario()
            {
                Login = login,
                Passwd = pwd,
                Roles = new List<Rol>() { new Rol() { Nombre = "Admin" } }
            };

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
    }
}
