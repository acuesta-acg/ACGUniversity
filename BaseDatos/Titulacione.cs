using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Titulacione
    {
        public Titulacione()
        {
            Cursos = new HashSet<Curso>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Curso> Cursos { get; set; }
    }
}
