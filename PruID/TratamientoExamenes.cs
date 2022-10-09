using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruID
{
    public class TratamientoExamenes
    {
        private readonly IClienteMail _clienteMail;
        private readonly ISerAlumnos _serAlumnos;

        public TratamientoExamenes(IClienteMail clienteMail, ISerAlumnos servAlumnos)
        {
            _clienteMail = clienteMail;
            _serAlumnos = servAlumnos;
        }

        public void MandarNotas()
        {
            foreach (var a in _serAlumnos.Lista())
                _clienteMail.EnviarMail(a.Mail, "Las notas", $"El alumno {a.Nombre}, ha obtenido una nota de {a.Nota} ");
        }
    }
}
