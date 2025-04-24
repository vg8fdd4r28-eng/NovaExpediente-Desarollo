using FluentValidation;

namespace NovaExpediente.Application.OneSignalAPP.Commands.SendNotification
{
    public class SendNotificationCommandValidator : AbstractValidator <SendNotificationCommand> 
    {
        public SendNotificationCommandValidator()
        {
            
        }
    }
}
