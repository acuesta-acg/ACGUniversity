using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruID
{
    public class LogTratamientoExamenes
    {
        private readonly IServTransient _transient;
        private readonly IServScope _scope;
        private readonly IServSingleton _singleton;
        public LogTratamientoExamenes(IServTransient transient, IServScope scope, IServSingleton singleton)
        {
            _transient = transient;
            _scope = scope;
            _singleton = singleton;
        }

        public void Informac(string scope)
        {
            LogOperacion(_transient, scope, "");
            LogOperacion(_scope, scope, "");
            LogOperacion(_singleton, scope, "");
        }
        public static void LogOperacion<T>(T operacion, string scope, string msg) where T : IIdent =>
            //Console.WriteLine($"{scope}: {typeof(T).Name,-19} [ {operacion.OperacionId}...{msg, -23}]");
            Console.WriteLine($"{scope}: {typeof(T).Name,-19} [ {operacion.OperacionId}...{msg}]");
    }
}
