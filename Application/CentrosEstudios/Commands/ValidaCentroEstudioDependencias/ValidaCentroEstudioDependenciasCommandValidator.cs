using FluentValidation;

namespace NovaExpediente.Application.CentrosEstudios.Commands.ValidaCentroEstudioDependencias
{
    class ValidaCentroEstudioDependenciasCommandValidator : AbstractValidator<ValidaCentroEstudioDependenciasCommand>
    {

        ValidaCentroEstudioDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
