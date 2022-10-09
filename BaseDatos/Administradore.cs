using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Administradore
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }

        public virtual DatosPersonale Persona { get; set; } = null!;
    }
}
