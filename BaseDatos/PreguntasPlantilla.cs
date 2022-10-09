using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class PreguntasPlantilla
    {
        public PreguntasPlantilla()
        {
            RespuestaPlantillas = new HashSet<RespuestaPlantilla>();
        }

        public int Id { get; set; }
        public string Texto { get; set; } = null!;
        public int PlantillaExamenId { get; set; }

        public virtual PlantillasExaman PlantillaExamen { get; set; } = null!;
        public virtual ICollection<RespuestaPlantilla> RespuestaPlantillas { get; set; }
    }
}
