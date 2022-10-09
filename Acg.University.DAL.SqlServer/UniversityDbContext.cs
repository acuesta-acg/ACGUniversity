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
        public virtual DbSet<CuantosTelPersona> CuantosTelefonos {get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder opciones)
        {
            if (!opciones.IsConfigured)
                opciones.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ACGUniversity;Integrated Security=True");

            opciones.LogTo(x => Console.WriteLine(x));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
