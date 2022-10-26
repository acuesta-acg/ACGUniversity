using Acg.University.BL.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcgUniversity.BL.Servicios.Test
{
    public class OperacionesTest
    {
        [Fact]
        public void Sumar_dos_numeros_y_revisar_resultado()
        {
            // Inicializar
            var op = new Operaciones();

            int numero_a = 1;
            int numero_b = 1;

            int resultado = 2;

            // Prueba

            var r = op.Sumar(numero_a, numero_b);

            // Revisión

            Assert.Equal(r, resultado);
        }

        [Fact]
        public void Dividir_dos_numeros_y_revisar_resultado()
        {
            // Inicializar
            var op = new Operaciones();

            int numero_a = 1;
            int numero_b = 1;

            int resultado = 1;

            // Prueba

            var r = op.Dividir(numero_a, numero_b);

            // Revisión
            Assert.Equal(r, resultado);
        }

        [Fact]
        public void Dividir_un_numero_entre_cero()
        {
            // Inicializar
            var op = new Operaciones();

            int numero_a = 1;
            int numero_b = 0;

            // Revisión
            Assert.Throws<DivideByZeroException>(() => op.Dividir(numero_a, numero_b));
        }
    }
}
