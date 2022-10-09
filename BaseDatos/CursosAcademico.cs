using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class CursosAcademico
    {
        public CursosAcademico()
        {
            Examenes = new HashSet<Examene>();
            Matriculas = new HashSet<Matricula>();
            ProfesoresAsignaturas = new HashSet<ProfesoresAsignatura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public virtual ICollection<Examene> Examenes { get; set; }
        public virtual ICollection<Matricula> Matriculas { get; set; }
        public virtual ICollection<ProfesoresAsignatura> ProfesoresAsignaturas { get; set; }
    }
}
