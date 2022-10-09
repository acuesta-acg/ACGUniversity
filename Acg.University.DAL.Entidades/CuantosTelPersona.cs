using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Keyless]
    public class CuantosTelPersona
    {
        public string Nombre { get; set; }
        
        public int nTel { get; set; }
    }
}
