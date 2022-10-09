using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class RespuestaPlantilla
    {
        public int Id { get; set; }
        public string Texto { get; set; } = null!;
        public bool Correcta { get; set; }
        public int PreguntaPlantillaId { get; set; }

        public virtual PreguntasPlantilla PreguntaPlantilla { get; set; } = null!;
    }
}
