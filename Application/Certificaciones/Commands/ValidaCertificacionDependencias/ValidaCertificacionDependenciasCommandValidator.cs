using FluentValidation;

namespace NovaExpediente.Application.Certificaciones.Commands.ValidaCertificacionDependencias
{
    class ValidaCertificacionDependenciasCommandValidator : AbstractValidator<ValidaCertificacionDependenciasCommand>
    {

        ValidaCertificacionDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
