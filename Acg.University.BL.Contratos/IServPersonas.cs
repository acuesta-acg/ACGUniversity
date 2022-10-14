using Acg.University.DAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.BL.Contratos
{
    public interface IServPersonas
    {
        #region Gestión de personas
        int NuevaPersona(
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
            string rol);

        Task<int> NuevaPersonaAsync(
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
            string rol);

        Persona? ConsultarPersona(int id);
        Task<Persona?> ConsultarPersonaAsync(int id);
        Persona? ConsultarPersona(string dni);
        Task<Persona?> ConsultarPersonaAsync(string dni);
        List<Persona> ListaPersonas();
        Task<List<Persona>> ListaPersonasAsync();
        int ModificarPersona(
            int id,
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel);
        Task<int> ModificarPersonaAsync(
            int id,
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel);
        void BorrarPersona(int id);
        Task BorrarPersonaAsync(int id);

        #endregion

        // Parte profesor
        #region Gestión de profesores

        int NuevoProfesor(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd);

        Task<int> NuevoProfesorAsync(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd);

        Profesor? ConsultarProfesor(int id);
        Task<Profesor?> ConsultarProfesorAsync(int id);
        Profesor? ConsultarProfesor(string dni);
        Task<Profesor?> ConsultarProfesorAsync(string dni);
        List<Profesor> ListaProfesores();
        Task<List<Profesor>> ListaProfesoresAsync();
        void BorrarProfesor(int id);
        Task BorrarProfesorAsync(int id);

        #endregion

        #region Gestión de Alumnos

        int NuevoAlumno(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd);

        Task<int> NuevoAlumnoAsync(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd);

        Alumno? ConsultarAlumno(int id);
        Task<Alumno?> ConsultarAlumnoAsync(int id);
        Alumno? ConsultarAlumno(string dni);
        Task<Alumno?> ConsultarAlumnoAsync(string dni);
        List<Alumno> ListaAlumnos();
        Task<List<Alumno>> ListaAlumnosAsync();
        void BorrarAlumno(int id);
        Task BorrarAlumnoAsync(int id);

        #endregion

        #region Gestión de Administradores

        int NuevoAdministrador(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd);

        Task<int> NuevoAdministradorAsync(
            string dni,
            string nombre,
            string direc,
            string poblac,
            string prov,
            string cp,
            string mail,
            string tel,
            string login,
            string pwd);

        Administrador? ConsultarAdministrador(int id);
        Task<Administrador?> ConsultarAdministradorAsync(int id);
        Administrador? ConsultarAdministrador(string dni);
        Task<Administrador?> ConsultarAdministradorAsync(string dni);
        List<Administrador> ListaAdministradores();
        Task<List<Administrador>> ListaAdministradoresAsync();
        void BorrarAdministrador(int id);
        Task BorrarAdministradorAsync(int id);

        #endregion

        #region Gestión de Investigadores
        #endregion
    }
}
