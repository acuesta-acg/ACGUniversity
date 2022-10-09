using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Matricula
    {
        public int CursoAcademicoId { get; set; }
        public int AlumnoId { get; set; }
        public int AsignaturaId { get; set; }

        public virtual Alumno Alumno { get; set; } = null!;
        public virtual Asignatura Asignatura { get; set; } = null!;
        public virtual CursosAcademico CursoAcademico { get; set; } = null!;
    }
}
