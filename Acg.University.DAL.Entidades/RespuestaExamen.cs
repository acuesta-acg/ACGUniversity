using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "RespuestasExamen")]
    public class RespuestaExamen
    {
        [Key]
        public int Id { get; set; }
        public int ExamenAlumnoId { get; set; }
        public int IdPregunta { get; set; }
        public int Respuesta { get; set; }
        public bool Correcta { get; set; }
    }
}
