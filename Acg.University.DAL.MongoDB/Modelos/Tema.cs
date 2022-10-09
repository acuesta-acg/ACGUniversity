using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.MongoDB.Modelos
{
    public class Tema
    {
        public string Titulo { get; set; }
        public List<String> Parrafos { get; set; }
        public List<String> Etiqueta { get; set; }
    }
}
