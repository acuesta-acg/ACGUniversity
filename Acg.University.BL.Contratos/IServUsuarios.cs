using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acg.University.DAL.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Acg.University.BL.Contratos
{
    public interface IServUsuarios
    {
        int NuevoUsuario(string login, string pwd);
        Task<int> NuevoUsuarioAsync(string login, string pwd);
        int NuevoUsuario(string login, string pwd, string rol);
        Task<int> NuevoUsuarioAsync(string login, string pwd, string rol);
        Usuario? ConsultarUsuario(int id);
        Task<Usuario?> ConsultarUsuarioAsync(int id);
        Usuario? ConsultarUsuario(string login);
        Task<Usuario?> ConsultarUsuarioAsync(string login);
        Usuario? Login(string login, string pwd);
        Task<Usuario?> LoginAsync(string login, string pwd);
        Usuario? LoginMalo(string login, string pwd);
        Task<Usuario?> LoginMaloAsync(string login, string pwd);
        List<Usuario> ListaUsuarios();
        Task<List<Usuario>> ListaUsuariosAsync();
        void BorrarUsuario(int id);
        Task BorrarUsuarioAsync(int id);
        void BorrarUsuario(string login);
        Task BorrarUsuarioAsync(string login);
        void ModificarPwd(int id, string pwdNueva);
        Task ModificarPwdAsync(int id, string pwdNueva);
        void AsignarRolUsuario(int idUsr, string rol);
        Task AsignarRolUsuarioAsync(int idUsr, string rol);
        void AsignarRolUsuario(string login, string rol);
        Task AsignarRolUsuarioAsync(string login, string rol);
        void BorrarRolUsuario(int idUsr, string rol);
        Task BorrarRolUsuarioAsync(int idUsr, string rol);
        void BorrarRolUsuario(string login, string rol);
        Task BorrarRolUsuarioAsync(string login, string rol);
        List<Rol> ListaRoles();
        Task<List<Rol>> ListaRolesAsync();
        Rol? ConsultarRol(int id);
        Task<Rol?> ConsultarRolAsync(int id);
        int NuevoRol(string rol);
        Task<int> NuevoRolAsync(string rol);
        void BorrarRol(string rol);
        Task BorrarRolAsync(string rol);
    }
}
