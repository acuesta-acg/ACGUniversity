using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Asignatura
    {
        public Asignatura()
        {
            Examenes = new HashSet<Examene>();
            Matriculas = new HashSet<Matricula>();
            PlantillasExamen = new HashSet<PlantillasExaman>();
            ProfesoresAsignaturas = new HashSet<ProfesoresAsignatura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CursoId { get; set; }

        public virtual Curso Curso { get; set; } = null!;
        public virtual ICollection<Examene> Examenes { get; set; }
        public virtual ICollection<Matricula> Matriculas { get; set; }
        public virtual ICollection<PlantillasExaman> PlantillasExamen { get; set; }
        public virtual ICollection<ProfesoresAsignatura> ProfesoresAsignaturas { get; set; }
    }
}
