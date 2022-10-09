using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class ProfesoresAsignatura
    {
        public int CursoAcademicoId { get; set; }
        public int ProfesorId { get; set; }
        public int AsignaturaId { get; set; }

        public virtual Asignatura Asignatura { get; set; } = null!;
        public virtual CursosAcademico CursoAcademico { get; set; } = null!;
        public virtual Profesore Profesor { get; set; } = null!;
    }
}
