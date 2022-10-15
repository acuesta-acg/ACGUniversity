namespace UniversityWeb.Serv
{
    public class ServicioApi : IServiceApi
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public ServicioApi(IHttpClientFactory httpClientBuilder) => _httpClientBuilder = httpClientBuilder;

        public List<Object> ConsultarAdministradores()
        {
            using var c2 = _httpClientBuilder.CreateClient();

            return null;

        }
    }
}
