using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    public class ProfesorAsignatura
    {
        public int CursoAcademicoId { get; set; }
        public int ProfesorId { get; set; }
        public int AsignaturaId { get; set; }
        public Profesor Profesor { get; set; }
        public Asignatura Asignatura { get; set; }
    }
}
