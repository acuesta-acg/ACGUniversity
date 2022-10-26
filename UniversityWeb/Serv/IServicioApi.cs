using System.Security.Claims;
using UniversityWeb.Models;

namespace UniversityWeb.Serv
{
    public interface IServicioApi
    {
        Task<ClaimsPrincipal> Login(string usr, string pwd);
        Task<List<InfoPersonalVM>> ConsultarAdministradoresAsync();
        void EnviarMsg(string msg); 
    }
}
