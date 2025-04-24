using FluentValidation;

namespace NovaExpediente.Application.Paises.Commands.UpsertPais
{
    class UpsertPaisCommandValidator : AbstractValidator<UpsertPaisCommand>
    {

        public UpsertPaisCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }
    }
}
