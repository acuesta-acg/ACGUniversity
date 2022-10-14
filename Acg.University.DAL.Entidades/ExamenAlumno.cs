using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "ExamenesAlumnos")]
    public class ExamenAlumno
    {
        [Key]
        public int Id { get; set; }
        public int ExamenId { get; set; }
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        public List<RespuestaExamen> Respuestas { get; set; }
    }
}
