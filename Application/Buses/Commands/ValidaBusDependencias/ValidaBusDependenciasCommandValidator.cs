using FluentValidation;

namespace NovaExpediente.Application.Buses.Commands.ValidaBusDependencias
{
    class ValidaBusDependenciasCommandValidator : AbstractValidator<ValidaBusDependenciasCommand>
    {

        ValidaBusDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
