using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace UniversityWeb.Hubs
{

    //[Authorize(Roles = "Nuevo,Admin")]
    public class NuestroHub :Hub
    {
        public async Task EnviarMsg(string usr, string msg)
        {
            var txt = Context.User?.Identity?.Name ?? "";
            await Clients.Others.SendAsync("MensajeRecibido", usr, msg);
        }

        public async Task EnviarMsg2(string msg)
        {
            await Clients.All.SendAsync("MensajeRecibido", Context.User?.Identity?.Name ?? "", msg);
            //await Clients.Others.SendAsync("MensajeRecibido", Context.User?.Identity?.Name ?? "", msg);
        }
    }
}
