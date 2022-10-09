using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            Investigadores = new HashSet<Investigadore>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Investigadore> Investigadores { get; set; }
    }
}
