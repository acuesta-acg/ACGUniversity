using Acg.University.BL.Servicios;
using Acg.University.DAL.Entidades;
using Acg.University.DAL.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcgUniversity.BL.Servicios.Test
{
    public class ServUsuariosTest
    {
        [Fact]
        public void Crear_un_nuevo_usario()
        {
            var s = Servidor();

            var u1 = new Usuario()
            {
                Login = "Jose",
                Passwd = "HolaJose"
            };

            var r = s.NuevoUsuario(u1.Login, u1.Passwd);

            var u2 = s.ConsultarUsuario(r);

            Assert.Equal(u1.Login, u2.Login);   

        }

        [Theory]
        [InlineData("dsfas", "Hola")]
        [InlineData("asñlkfj", "Hola")]
        [InlineData("dsfasasdf", "Hola")]
        [InlineData("Juan", "Hola")]
        [InlineData("Jose", "Hola")]
        [InlineData("Pedro", "Hola")]
        public void Crear_nuevos_usuarios(string login, string pwd)
        {
            var s = Servidor();

            var r = s.NuevoUsuario(login, pwd);

            var usuario = s.ConsultarUsuario(r);

            Assert.Equal(usuario.Login, login);
        }

        private ServUsuarios Servidor() => new ServUsuarios(Conex());

        private UniversityDbContext Conex() => new UniversityDbContext(
                new DbContextOptionsBuilder<UniversityDbContext>()
                .UseInMemoryDatabase("BDUniversityPru").Options);
    }
}
