using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using UniversityWeb.Models;

namespace UniversityWeb.Serv
{
    public class ServicioApi : IServicioApi
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public ServicioApi(IHttpClientFactory httpClientBuilder) => _httpClientBuilder = httpClientBuilder;

        public async Task<ClaimsPrincipal> Login(string usr, string pwd)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(
                    new Credenciales() { login = usr, pwd = pwd }),
                Encoding.UTF8, "application/json");

            try
            {
                var r = await _httpClientBuilder.CreateClient().PostAsync("https://localhost:7214/api/Login", body);
                if (r.IsSuccessStatusCode)
                {
                    if (r.StatusCode == HttpStatusCode.OK)
                    {
                        var jwt = await r.Content.ReadAsStringAsync();
                        var jwt2 = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

                        return new ClaimsPrincipal(new ClaimsIdentity(jwt2.Claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<List<InfoPersonalVM>> ConsultarAdministradoresAsync()
        {
            try
            {
                using (var r = await _httpClientBuilder.CreateClient()
                    .GetAsync("https://localhost:7214/api/Administradores"))
                {
                    if (r.IsSuccessStatusCode)
                    {
                        if (r.StatusCode == HttpStatusCode.OK)
                        {
                            var l = JsonSerializer.Deserialize<List<InfoPersonalVM>>(await r.Content.ReadAsStringAsync());
                            return l ?? new List<InfoPersonalVM>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<InfoPersonalVM>();
        }

        public void EnviarMsg(string msg)
        {
            var cf = new ConnectionFactory() { HostName = "localhost" };

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
            }
        }
    }

    public class Credenciales
    {
        public string login { get; set; }
        public string pwd { get; set; }
    }
}
