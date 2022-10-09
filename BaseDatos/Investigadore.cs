using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Investigadore
    {
        public Investigadore()
        {
            Proyectos = new HashSet<Proyecto>();
        }

        public int Id { get; set; }
        public int PersonaId { get; set; }

        public virtual DatosPersonale Persona { get; set; } = null!;

        public virtual ICollection<Proyecto> Proyectos { get; set; }
    }
}
