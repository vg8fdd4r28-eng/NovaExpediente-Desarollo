using FluentValidation;

namespace NovaExpediente.Application.Sectores.Commands.ValidaSectorDependencias
{
    class ValidaSectorDependenciasCommandValidator : AbstractValidator<ValidaSectorDependenciasCommand>
    {

        ValidaSectorDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
