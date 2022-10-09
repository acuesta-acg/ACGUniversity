using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruID
{
    public class ServAlumnos : ISerAlumnos
    {
        public List<Alumno> Lista()
        {
            return new List<Alumno>()
            {
                new Alumno { Nombre ="adsfasdfas", Mail = "asdfasdf@adsfasdf.es", Nota =5},
                new Alumno { Nombre ="adsfasdfasd", Mail = "asdfasdf@asdf.es", Nota =6},
                new Alumno { Nombre ="asdfas", Mail = "asdfas@eara.es", Nota =7},
                new Alumno { Nombre ="afsasdf", Mail = "asdfasdf@3ewerwe.es", Nota =8}
            };
        }
    }

    public class Alumno
    {
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public int Nota { get; set; }
    }
}
