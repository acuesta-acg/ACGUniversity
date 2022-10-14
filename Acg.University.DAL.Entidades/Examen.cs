using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "Examenes")]
    public class Examen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CursoAcademicoId { get; set; }
        public int AsignaturaId { get; set; }
        public int ProfesorId { get; set; }
        public int PreguntasMin { get; set; }
        public int PreguntasMax { get; set; }
        public CursoAcademico Curso { get; set; }
        public Asignatura Asignatura { get; set; }
        public Profesor Profesor { get; set; }
    }
}
