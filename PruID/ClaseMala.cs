using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruID
{
    public class ClaseMala : IServTransient
    {
        public void MandarNotas()
        {
            Console.WriteLine("soy una clase Mala");
        }

        public string OperacionId { get; } = Guid.NewGuid().ToString();
    }
}
