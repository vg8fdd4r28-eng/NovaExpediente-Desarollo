using FluentValidation;

namespace NovaExpediente.Application.Conversaciones.Commands.InsertConversacionMensajes
{
    public class InsertConversacionMensajeCommandValidator : AbstractValidator<InsertConversacionMensajeCommand>
    {
        public InsertConversacionMensajeCommandValidator()
        {
            RuleFor(x => x.conversationId).NotEmpty();
            RuleFor(x => x.userId).NotEmpty();
            RuleFor(x => x.message).NotEmpty();
        }
    }
}
