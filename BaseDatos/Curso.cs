using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Curso
    {
        public Curso()
        {
            Asignaturas = new HashSet<Asignatura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int TitulacionId { get; set; }

        public virtual Titulacione Titulacion { get; set; } = null!;
        public virtual ICollection<Asignatura> Asignaturas { get; set; }
    }
}
