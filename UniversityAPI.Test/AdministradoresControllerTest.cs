using Acg.University.BL.Contratos;
using Acg.University.BL.Servicios;
using Acg.University.DAL.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAPI.Controllers;

namespace UniversityAPI.Test
{
    public class AdministradoresControllerTest
    {
        /*
        [Fact]
        public async void Revisar_lista_de_administradores()
        {
            var controlador = Controlador();

            IActionResult lista = await controlador.Lista();

            var notFound = lista as NotFoundResult;

            Assert.NotNull(lista);
            Assert.Equal(404, notFound?.StatusCode);

        }


        [Fact]
        public async void Nuevo_admninistrador()
        {
            var controlador = Controlador();

            IActionResult r = await controlador.Nuevo("dni", "nombre", "direc", "dsaf", "sdf", "asdf", "", "", "login", "pwd");

            var ok = r as OkResult;

            Assert.NotNull(ok);
            Assert.Equal(200, ok?.StatusCode);

        } */

        [Fact]
        public async void Revisar_lista_de_administradores_con_datos()
        {
            var controlador = Controlador();

            IActionResult r = await controlador.Nuevo("dni", "usuario", "daf", "dsaf", "sdf", "asdf", "", "", "", "");

            var ok = r as OkResult;

            Assert.NotNull(ok);
            Assert.Equal(200, ok?.StatusCode);

            IActionResult lista = await controlador.Lista();

            Assert.NotNull(lista);
        }

        private AdministradoresController Controlador() => new AdministradoresController(Servidor());
        
        private ServPersonas Servidor() => new ServPersonas(Conex());

        private UniversityDbContext Conex() => new UniversityDbContext(
                new DbContextOptionsBuilder<UniversityDbContext>()
                .UseInMemoryDatabase("BDUniversityPru").Options);
        
    }
}
