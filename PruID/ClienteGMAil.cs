using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruID
{
    internal class ClienteGMAil : IClienteMail
    {
        public ClienteGMAil()
        {
            // Inicializamos todo
        }

        public void EnviarMail(string mail, string asunto, string texto)
        {
            Console.WriteLine("----  Enviando correos con GMail -------");
            Console.WriteLine($"Enviando mail a: {mail}" );
            Console.WriteLine($"Asunto mail a: {asunto}");
            Console.WriteLine($"Texto mail a: {texto}");
            Console.WriteLine("--------------------------------");
        }
    }
}
