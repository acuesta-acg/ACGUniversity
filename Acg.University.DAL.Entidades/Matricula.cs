using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "Matriculas")]
    public class Matricula
    {
        public int CursoAcademicoId { get; set; }
        //[Key, Column(Order = 1)]
        public int AlumnoId { get; set; }
        //[Key, Column(Order = 2)]
        public int AsignaturaId { get; set; }
        public CursoAcademico Curso { get; set; }
        public Alumno Alumno { get; set; }
        public Asignatura Asignatura { get; set; }
    }
}
