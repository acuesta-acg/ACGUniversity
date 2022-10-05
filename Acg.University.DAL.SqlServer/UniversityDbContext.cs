using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acg.University.DAL.Entidades;

namespace Acg.University.DAL.SqlServer
{
    public class UniversityDbContext : DbContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder opciones)
        {
            if (!opciones.IsConfigured)
                opciones.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ACGUniversity;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Usuario>(e =>
            {
                e.HasIndex(e => e.Login).IsUnique();
            }); */
        }
    }
}
