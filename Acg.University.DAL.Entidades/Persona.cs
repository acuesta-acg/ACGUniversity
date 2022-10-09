using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion {  get; set; }
        public string Poblacion { get; set; }
        public string Provincia { get; set; }
        public Usuario? Usuario { get; set; }
        public List<TelefonoPersona> Telefonos { get; set; }

    }
}
