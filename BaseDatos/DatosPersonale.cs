using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class DatosPersonale
    {
        public int Id { get; set; }
        public string Dni { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Poblacion { get; set; } = null!;
        public string Provincia { get; set; } = null!;
        public string CodPostal { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Administradore? Administradore { get; set; }
        public virtual Alumno? Alumno { get; set; }
        public virtual Investigadore? Investigadore { get; set; }
        public virtual Profesore? Profesore { get; set; }
    }
}
