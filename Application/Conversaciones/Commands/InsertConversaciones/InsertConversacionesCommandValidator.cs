using FluentValidation;

namespace NovaExpediente.Application.Conversaciones.Commands.InsertConversaciones
{
    public class InsertConversacionesCommandValidator : AbstractValidator<InsertConversacionesCommand>
    {
        public InsertConversacionesCommandValidator()
        {
            RuleFor(x => x.userId).NotEmpty();
        }
    }
}
