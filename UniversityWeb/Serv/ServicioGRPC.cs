using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Grpc.Core;
using Grpc.Net.Client;
using UniversityGRPC;
using UniversityWeb.Models;

namespace UniversityWeb.Serv
{
    public class ServicioGRPC : IServicioApi
    {
        public async Task<ClaimsPrincipal> Login(string usr, string pwd)
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:7262"))
            {
                var client = new University.UniversityClient(channel);
                var jwt = client.Login(new UniversityGRPC.Credenciales() { Login = usr, Passwd = pwd });

                if (jwt.Token != string.Empty)
                {
                    var jwt2 = new JwtSecurityTokenHandler().ReadJwtToken(jwt.Token);

                    return new ClaimsPrincipal(new ClaimsIdentity(jwt2.Claims, CookieAuthenticationDefaults.AuthenticationScheme));
                }
            }

            return null;
        }
        public async Task<List<InfoPersonalVM>> ConsultarAdministradoresAsync()
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:7262"))
            {
                var client = new University.UniversityClient(channel);
                var l = await client.ListaAdministradoresAsync(new Nada());

                return l.Lista.Select(x => new InfoPersonalVM()
                {
                    id = x.Id,
                    dni = x.Dni,
                    nombre = x.Nombre,
                    direc = x.Direc,
                    poblac = x.Poblac,
                    prov = x.Prov
                }).ToList();
            }

            return null;
        }

        public void EnviarMsg(string msg)
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:7262"))
            {
                var client = new Greeter.GreeterClient(channel);
                var reply = client.SayHello(
                                  new HelloRequest { Name = "Hola soy Coco..." });

                Console.WriteLine("Greeting: " + reply.Message);
            }

            var cf = new ConnectionFactory() { HostName = "localhost" };

            /*
            using (var connection = cf.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        "hola",
                        true,
                        false,
                        false,
                        null);

                    var cuerpo = Encoding.UTF8.GetBytes(msg);

                    var prop = channel.CreateBasicProperties();
                    prop.Persistent = true;

                    channel.BasicPublish("", "hola", prop, cuerpo);
                    //channel.BasicPublish("", "hola", null, cuerpo);
                    //  Console.WriteLine($"Enviado el mensaje {msg}");
                }
            } */
        }
    }

}
