using FluentValidation;

namespace NovaExpediente.Application.UnosAUnos.Commands.UpsertUnosaUnosDetalles
{
    public class UpsertUnosAUnosDetallesCommandValidator : AbstractValidator<UpsertUnosAUnosDetallesCommand>
    {
        public UpsertUnosAUnosDetallesCommandValidator()
        {
            RuleFor(x => x.data).NotEmpty();
        }
    }
}
