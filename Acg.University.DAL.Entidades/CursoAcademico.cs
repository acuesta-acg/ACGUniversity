using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "CursosAcademicos")]
    public class CursoAcademico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<ProfesorAsignatura> ProfesoresAsignaturas { get; set; }
        public List<Matricula> Matricula { get; set; }
        public List<Examen> Examenes { get; set; }
    }
}
