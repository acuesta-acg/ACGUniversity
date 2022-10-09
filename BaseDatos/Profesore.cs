using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Profesore
    {
        public Profesore()
        {
            Examenes = new HashSet<Examene>();
            PlantillasExamen = new HashSet<PlantillasExaman>();
            ProfesoresAsignaturas = new HashSet<ProfesoresAsignatura>();
        }

        public int Id { get; set; }
        public int PersonaId { get; set; }

        public virtual DatosPersonale Persona { get; set; } = null!;
        public virtual ICollection<Examene> Examenes { get; set; }
        public virtual ICollection<PlantillasExaman> PlantillasExamen { get; set; }
        public virtual ICollection<ProfesoresAsignatura> ProfesoresAsignaturas { get; set; }
    }
}
