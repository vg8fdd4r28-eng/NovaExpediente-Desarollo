using FluentValidation;

namespace NovaExpediente.Application.TiposCore.Commands.ValidaTipoCoreDependencias
{
    class ValidaTipoCoreDependenciasCommandValidator : AbstractValidator<ValidaTipoCoreDependenciasCommand>
    {

        ValidaTipoCoreDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
