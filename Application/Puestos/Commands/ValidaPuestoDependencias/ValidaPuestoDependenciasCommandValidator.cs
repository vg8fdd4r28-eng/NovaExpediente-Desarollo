using FluentValidation;

namespace NovaExpediente.Application.Puestos.Commands.ValidaPuestoDependencias
{
    class ValidaPuestoDependenciasCommandValidator : AbstractValidator<ValidaPuestoDependenciasCommand>
    {

        ValidaPuestoDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
