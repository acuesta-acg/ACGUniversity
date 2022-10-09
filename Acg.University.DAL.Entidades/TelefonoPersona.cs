using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    public class TelefonoPersona
    {
        public int Id { get; set; }
        public string Numero { get; set; }

        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
    }
}
