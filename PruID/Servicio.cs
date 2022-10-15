using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruID
{
    public class Servicio : IServTransient, IServSingleton, IServScope
    {
        public string OperacionId { get; } = Guid.NewGuid().ToString()[^4..];
    }
}
