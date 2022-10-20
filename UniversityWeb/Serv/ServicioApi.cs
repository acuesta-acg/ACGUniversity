using System.Net;
using System.Text.Json;
using UniversityWeb.Models;

namespace UniversityWeb.Serv
{
    public class ServicioApi : IServicioApi
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public ServicioApi(IHttpClientFactory httpClientBuilder) => _httpClientBuilder = httpClientBuilder;


        public async Task<List<InfoPersonalVM>> ConsultarAdministradoresAsync()
        {
            try
            {
                using (var r = await _httpClientBuilder.CreateClient().GetAsync("https://localhost:7214/api/Administradores"))
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
    }
}
