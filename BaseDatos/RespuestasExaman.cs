using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class RespuestasExaman
    {
        public int Id { get; set; }
        public int ExamenAlumnoId { get; set; }
        public int IdPregunta { get; set; }
        public int Respuesta { get; set; }
        public bool Correcta { get; set; }

        public virtual ExamenesAlumno ExamenAlumno { get; set; } = null!;
    }
}
