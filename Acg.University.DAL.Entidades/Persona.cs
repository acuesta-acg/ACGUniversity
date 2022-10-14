using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(12)]
        public string DNI { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string Provincia { get; set; }
        public string CodPostal { get; set; }
        public string EMail { get; set; }
        public Usuario? Usuario { get; set; }
        public List<TelefonoPersona> Telefonos { get; set; }
    }
}
