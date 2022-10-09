using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BaseDatos
{
    public partial class AcgUniversityContext : DbContext
    {
        public AcgUniversityContext()
        {
        }

        public AcgUniversityContext(DbContextOptions<AcgUniversityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administradore> Administradores { get; set; } = null!;
        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Asignatura> Asignaturas { get; set; } = null!;
        public virtual DbSet<Curso> Cursos { get; set; } = null!;
        public virtual DbSet<CursosAcademico> CursosAcademicos { get; set; } = null!;
        public virtual DbSet<DatosPersonale> DatosPersonales { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Examene> Examenes { get; set; } = null!;
        public virtual DbSet<ExamenesAlumno> ExamenesAlumnos { get; set; } = null!;
        public virtual DbSet<Investigadore> Investigadores { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Matricula> Matriculas { get; set; } = null!;
        public virtual DbSet<PlantillasExaman> PlantillasExamen { get; set; } = null!;
        public virtual DbSet<PreguntasPlantilla> PreguntasPlantillas { get; set; } = null!;
        public virtual DbSet<Profesore> Profesores { get; set; } = null!;
        public virtual DbSet<ProfesoresAsignatura> ProfesoresAsignaturas { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyectos { get; set; } = null!;
        public virtual DbSet<RespuestaPlantilla> RespuestaPlantillas { get; set; } = null!;
        public virtual DbSet<RespuestasExaman> RespuestasExamen { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Titulacione> Titulaciones { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<ViewProfesoresAsignatura> ViewProfesoresAsignaturas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=AcgUniversity;User ID=sa;Password=HolaCoco$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administradore>(entity =>
            {
                entity.HasIndex(e => e.PersonaId, "IX_Administradores_PersonaId")
                    .IsUnique();

                entity.HasOne(d => d.Persona)
                    .WithOne(p => p.Administradore)
                    .HasForeignKey<Administradore>(d => d.PersonaId);
            });

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasIndex(e => e.PersonaId, "IX_Alumnos_PersonaId")
                    .IsUnique();

                entity.HasOne(d => d.Persona)
                    .WithOne(p => p.Alumno)
                    .HasForeignKey<Alumno>(d => d.PersonaId);
            });

            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.HasIndex(e => e.CursoId, "IX_Asignaturas_CursoId");

                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.Asignaturas)
                    .HasForeignKey(d => d.CursoId);
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasIndex(e => e.TitulacionId, "IX_Cursos_TitulacionId");

                entity.HasOne(d => d.Titulacion)
                    .WithMany(p => p.Cursos)
                    .HasForeignKey(d => d.TitulacionId);
            });

            modelBuilder.Entity<DatosPersonale>(entity =>
            {
                entity.HasIndex(e => e.Dni, "IX_DatosPersonales_DNI")
                    .IsUnique();

                entity.HasIndex(e => e.UsuarioId, "IX_DatosPersonales_UsuarioId");

                entity.Property(e => e.Dni).HasColumnName("DNI");

                entity.Property(e => e.Email).HasColumnName("EMail");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.DatosPersonales)
                    .HasForeignKey(d => d.UsuarioId);
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasIndex(e => e.Nombre, "IX_Departamentos_Nombre")
                    .IsUnique();
            });

            modelBuilder.Entity<Examene>(entity =>
            {
                entity.HasIndex(e => e.AsignaturaId, "IX_Examenes_AsignaturaId");

                entity.HasIndex(e => e.CursoAcademicoId, "IX_Examenes_CursoAcademicoId");

                entity.HasIndex(e => e.ProfesorId, "IX_Examenes_ProfesorId");

                entity.HasOne(d => d.Asignatura)
                    .WithMany(p => p.Examenes)
                    .HasForeignKey(d => d.AsignaturaId);

                entity.HasOne(d => d.CursoAcademico)
                    .WithMany(p => p.Examenes)
                    .HasForeignKey(d => d.CursoAcademicoId);

                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.Examenes)
                    .HasForeignKey(d => d.ProfesorId);
            });

            modelBuilder.Entity<ExamenesAlumno>(entity =>
            {
                entity.HasIndex(e => e.AlumnoId, "IX_ExamenesAlumnos_AlumnoId");

                entity.HasOne(d => d.Alumno)
                    .WithMany(p => p.ExamenesAlumnos)
                    .HasForeignKey(d => d.AlumnoId);
            });

            modelBuilder.Entity<Investigadore>(entity =>
            {
                entity.HasIndex(e => e.PersonaId, "IX_Investigadores_PersonaId")
                    .IsUnique();

                entity.HasOne(d => d.Persona)
                    .WithOne(p => p.Investigadore)
                    .HasForeignKey<Investigadore>(d => d.PersonaId);

                entity.HasMany(d => d.Proyectos)
                    .WithMany(p => p.Investigadores)
                    .UsingEntity<Dictionary<string, object>>(
                        "InvestigadorProyecto",
                        l => l.HasOne<Proyecto>().WithMany().HasForeignKey("ProyectosId"),
                        r => r.HasOne<Investigadore>().WithMany().HasForeignKey("InvestigadoresId"),
                        j =>
                        {
                            j.HasKey("InvestigadoresId", "ProyectosId");

                            j.ToTable("InvestigadorProyecto");

                            j.HasIndex(new[] { "ProyectosId" }, "IX_InvestigadorProyecto_ProyectosId");
                        });
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.HasKey(e => new { e.CursoAcademicoId, e.AlumnoId, e.AsignaturaId });

                entity.HasIndex(e => e.AlumnoId, "IX_Matriculas_AlumnoId");

                entity.HasIndex(e => e.AsignaturaId, "IX_Matriculas_AsignaturaId");

                entity.HasOne(d => d.Alumno)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.AlumnoId);

                entity.HasOne(d => d.Asignatura)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.AsignaturaId);

                entity.HasOne(d => d.CursoAcademico)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.CursoAcademicoId);
            });

            modelBuilder.Entity<PlantillasExaman>(entity =>
            {
                entity.HasIndex(e => e.AsignaturaId, "IX_PlantillasExamen_AsignaturaId");

                entity.HasIndex(e => e.DepartamentoId, "IX_PlantillasExamen_DepartamentoId");

                entity.HasIndex(e => e.ProfesorId, "IX_PlantillasExamen_ProfesorId");

                entity.HasOne(d => d.Asignatura)
                    .WithMany(p => p.PlantillasExamen)
                    .HasForeignKey(d => d.AsignaturaId);

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.PlantillasExamen)
                    .HasForeignKey(d => d.DepartamentoId);

                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.PlantillasExamen)
                    .HasForeignKey(d => d.ProfesorId);
            });

            modelBuilder.Entity<PreguntasPlantilla>(entity =>
            {
                entity.ToTable("PreguntasPlantilla");

                entity.HasIndex(e => e.PlantillaExamenId, "IX_PreguntasPlantilla_PlantillaExamenId");

                entity.HasOne(d => d.PlantillaExamen)
                    .WithMany(p => p.PreguntasPlantillas)
                    .HasForeignKey(d => d.PlantillaExamenId);
            });

            modelBuilder.Entity<Profesore>(entity =>
            {
                entity.HasIndex(e => e.PersonaId, "IX_Profesores_PersonaId")
                    .IsUnique();

                entity.HasOne(d => d.Persona)
                    .WithOne(p => p.Profesore)
                    .HasForeignKey<Profesore>(d => d.PersonaId);
            });

            modelBuilder.Entity<ProfesoresAsignatura>(entity =>
            {
                entity.HasKey(e => new { e.CursoAcademicoId, e.ProfesorId, e.AsignaturaId });

                entity.HasIndex(e => e.AsignaturaId, "IX_ProfesoresAsignaturas_AsignaturaId");

                entity.HasIndex(e => e.ProfesorId, "IX_ProfesoresAsignaturas_ProfesorId");

                entity.HasOne(d => d.Asignatura)
                    .WithMany(p => p.ProfesoresAsignaturas)
                    .HasForeignKey(d => d.AsignaturaId);

                entity.HasOne(d => d.CursoAcademico)
                    .WithMany(p => p.ProfesoresAsignaturas)
                    .HasForeignKey(d => d.CursoAcademicoId);

                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.ProfesoresAsignaturas)
                    .HasForeignKey(d => d.ProfesorId);
            });

            modelBuilder.Entity<RespuestaPlantilla>(entity =>
            {
                entity.HasIndex(e => e.PreguntaPlantillaId, "IX_RespuestaPlantillas_PreguntaPlantillaId");

                entity.HasOne(d => d.PreguntaPlantilla)
                    .WithMany(p => p.RespuestaPlantillas)
                    .HasForeignKey(d => d.PreguntaPlantillaId);
            });

            modelBuilder.Entity<RespuestasExaman>(entity =>
            {
                entity.HasIndex(e => e.ExamenAlumnoId, "IX_RespuestasExamen_ExamenAlumnoId");

                entity.HasOne(d => d.ExamenAlumno)
                    .WithMany(p => p.RespuestasExamen)
                    .HasForeignKey(d => d.ExamenAlumnoId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Nombre, "IX_Roles_Nombre")
                    .IsUnique();

                entity.HasMany(d => d.Usuarios)
                    .WithMany(p => p.Roles)
                    .UsingEntity<Dictionary<string, object>>(
                        "RolUsuario",
                        l => l.HasOne<Usuario>().WithMany().HasForeignKey("UsuariosId"),
                        r => r.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
                        j =>
                        {
                            j.HasKey("RolesId", "UsuariosId");

                            j.ToTable("RolUsuario");

                            j.HasIndex(new[] { "UsuariosId" }, "IX_RolUsuario_UsuariosId");
                        });
            });

            modelBuilder.Entity<Titulacione>(entity =>
            {
                entity.HasIndex(e => e.Nombre, "IX_Titulaciones_Nombre")
                    .IsUnique();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Login, "IX_Usuarios_Login")
                    .IsUnique();
            });

            modelBuilder.Entity<ViewProfesoresAsignatura>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Profesores_asignaturas");

                entity.Property(e => e.Nprofesores).HasColumnName("NProfesores");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
