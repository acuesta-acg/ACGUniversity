using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruID
{
    public interface IClienteMail
    {
        void EnviarMail(string mail, string asunto, string texto);
    }
}
