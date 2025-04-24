using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface IOneSignalService
    {

        Task<string> ViewDevices();
        Task<string> ViewNotifications();
        Task<string> DeleteDevice(string Id);
        Task<string> SendNotification(string titulo,string msj,string[] segmentos, string todos);
    }
}
