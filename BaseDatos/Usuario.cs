using System;
using System.Collections.Generic;

namespace BaseDatos
{
    public partial class Usuario
    {
        public Usuario()
        {
            DatosPersonales = new HashSet<DatosPersonale>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Passwd { get; set; } = null!;

        public virtual ICollection<DatosPersonale> DatosPersonales { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
