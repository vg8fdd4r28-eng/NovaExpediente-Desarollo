using FluentValidation;

namespace NovaExpediente.Application.AmbientesDesarrollo.Commands.ValidaAmbienteDependencias
{
    class ValidaAmbienteDependenciasCommandValidator : AbstractValidator<ValidaAmbienteDependenciasCommand>
    {

        ValidaAmbienteDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
