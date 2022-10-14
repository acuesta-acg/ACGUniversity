using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "RespuestasPlantilla")]
    public class RespuestaPlantilla
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Texto { get; set; }
        public bool Correcta { get; set; }
        public int PreguntaPlantillaId { get; set; }
    }
}
