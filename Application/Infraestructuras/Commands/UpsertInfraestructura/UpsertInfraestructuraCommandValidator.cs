using FluentValidation;

namespace NovaExpediente.Application.Infraestructuras.Commands.UpsertInfraestructura
{
    public class UpsertInfraestructuraCommandValidator : AbstractValidator<UpsertInfraestructuraCommand>
    {

        public UpsertInfraestructuraCommandValidator()
        {

            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();

        }
    }
}
