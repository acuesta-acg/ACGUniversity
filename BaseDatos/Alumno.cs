using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Alumno
    {
        public Alumno()
        {
            ExamenesAlumnos = new HashSet<ExamenesAlumno>();
            Matriculas = new HashSet<Matricula>();
        }

        public int Id { get; set; }
        public int PersonaId { get; set; }

        public virtual DatosPersonale Persona { get; set; } = null!;
        public virtual ICollection<ExamenesAlumno> ExamenesAlumnos { get; set; }
        public virtual ICollection<Matricula> Matriculas { get; set; }
    }
}
