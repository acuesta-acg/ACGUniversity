using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Departamento
    {
        public Departamento()
        {
            PlantillasExamen = new HashSet<PlantillasExaman>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<PlantillasExaman> PlantillasExamen { get; set; }
    }
}
