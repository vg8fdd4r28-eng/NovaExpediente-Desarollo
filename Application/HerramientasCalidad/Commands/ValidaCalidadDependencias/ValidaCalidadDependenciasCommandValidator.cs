using FluentValidation;

namespace NovaExpediente.Application.HerramientasCalidad.Commands.ValidaCalidadDependencias
{
    class ValidaCalidadDependenciasCommandValidator : AbstractValidator<ValidaCalidadDependenciasCommand>
    {

        ValidaCalidadDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
