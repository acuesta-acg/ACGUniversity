using UniversityWeb.Models;

namespace UniversityWeb.Serv
{
    public interface IServicioApi
    {
        Task<List<InfoPersonalVM>> ConsultarAdministradoresAsync();
    }
}
