using FluentValidation;

namespace NovaExpediente.Application.ErpCores.Commands.ValidaERPDependencias
{
    class ValidaERPDependenciasCommandValidator : AbstractValidator<ValidaERPDependenciasCommand>
    {

        ValidaERPDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
