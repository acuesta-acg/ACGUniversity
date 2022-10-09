using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class ExamenesAlumno
    {
        public ExamenesAlumno()
        {
            RespuestasExamen = new HashSet<RespuestasExaman>();
        }

        public int Id { get; set; }
        public int ExamenId { get; set; }
        public int AlumnoId { get; set; }

        public virtual Alumno Alumno { get; set; } = null!;
        public virtual ICollection<RespuestasExaman> RespuestasExamen { get; set; }
    }
}
