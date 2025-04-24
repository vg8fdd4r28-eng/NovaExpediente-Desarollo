using NovaExpediente.Application.Notifications.Models;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(MessageDto message);
        Task SendAsync(MensajeSolicitudAdministrativa message);
    }
}
