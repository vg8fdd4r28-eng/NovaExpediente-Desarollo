using MediatR;

namespace NovaExpediente.Application.OneSignalAPP.Queries.GetNotifications
{
    public class GetNotificationsListQuery : IRequest<string>
    {
        public string cliente { get; set; }
    }
}
