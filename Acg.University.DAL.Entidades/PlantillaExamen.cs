using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "PlantillasExamen")]
    public class PlantillaExamen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AsignaturaId { get; set; }
        public int DepartamentoId { get; set; }
        public int ProfesorId { get; set; }
        public Asignatura Asignatura { get; set; }
        public Departamento Departamento { get; set; }
        public Profesor Profesor { get; set; }
        public List<PreguntaPlantilla> Preguntas { get; set; }
    }
}
