using FluentValidation;

namespace NovaExpediente.Application.Idiomas.Commands.ValidaIdiomaDependencias
{
    class ValidaIdiomaDependenciasCommandValidator : AbstractValidator<ValidaIdiomaDependenciasCommand>
    {

        ValidaIdiomaDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
