using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "Investigadores")]
    [Index(nameof(PersonaId), IsUnique = true)]
    public class Investigador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public List<Proyecto> Proyectos { get; set; }
    }
}
