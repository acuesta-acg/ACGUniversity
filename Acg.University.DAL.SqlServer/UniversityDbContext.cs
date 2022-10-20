using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acg.University.DAL.Entidades;
using System.Security.Cryptography.X509Certificates;

namespace Acg.University.DAL.SqlServer
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Titulacion> Titulaciones { get; set; }
        public DbSet<CuantosTelPersona> CuantosTelefonos {get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Investigador> Investigadores { get; set; }
        public DbSet<CursoAcademico> CursosAcademicos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Examen> Examenes { get; set; }
        public DbSet<ExamenAlumno> ExamenesAlumnos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<ProfesorAsignatura> ProfesoresAsignaturas { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<PlantillaExamen> PlantillasExamen { get; set; }
        public DbSet<PreguntaPlantilla> PreguntasPlantilla { get; set; }
        public DbSet<RespuestaPlantilla> RespuestaPlantillas { get; set; }

        public UniversityDbContext()
        {

        }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder opciones)
        {
            if (!opciones.IsConfigured)
                opciones.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ACGUniversity;Integrated Security=True");

            // opciones.LogTo(x => Console.WriteLine(x));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.ToTable("Matriculas");
                entity.HasKey(t => new { t.CursoAcademicoId, t.AlumnoId, t.AsignaturaId });
            });

            modelBuilder.Entity<ProfesorAsignatura>(entity =>
            {
                entity.ToTable("ProfesoresAsignaturas");
                entity.HasKey(t => new { t.CursoAcademicoId, t.ProfesorId, t.AsignaturaId });
            });

            modelBuilder.Entity<CuantosTelPersona>(e =>
            {
                e.HasNoKey();
                e.ToView("View_TelefonosPersonas");
            });
            /*modelBuilder.Entity<Usuario>(e =>
            {
                e.HasIndex(e => e.Login).IsUnique();
            }); */

            modelBuilder.Entity<Rol>().HasData(
                new Rol() { Id = 1, Nombre = "Admin" },
                new Rol() { Id = 2, Nombre = "Alumno" },
                new Rol() { Id = 3, Nombre = "Invest" },
                new Rol() { Id = 4, Nombre = "Profesor" });
        }
    }
}
