using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class PlantillasExaman
    {
        public PlantillasExaman()
        {
            PreguntasPlantillas = new HashSet<PreguntasPlantilla>();
        }

        public int Id { get; set; }
        public int AsignaturaId { get; set; }
        public int DepartamentoId { get; set; }
        public int ProfesorId { get; set; }

        public virtual Asignatura Asignatura { get; set; } = null!;
        public virtual Departamento Departamento { get; set; } = null!;
        public virtual Profesore Profesor { get; set; } = null!;
        public virtual ICollection<PreguntasPlantilla> PreguntasPlantillas { get; set; }
    }
}
