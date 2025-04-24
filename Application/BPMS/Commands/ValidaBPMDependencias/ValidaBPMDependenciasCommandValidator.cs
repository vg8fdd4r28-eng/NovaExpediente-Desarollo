using FluentValidation;

namespace NovaExpediente.Application.BPMS.Commands.ValidaBPMDependencias
{
    class ValidaBPMDependenciasCommandValidator : AbstractValidator<ValidaBPMDependenciasCommand>
    {

        ValidaBPMDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
