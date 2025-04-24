using FluentValidation;

namespace NovaExpediente.Application.CertificacionesProveedor.Commands.ValidaCertificacionProveedorDependencias
{
    class ValidaCertificacionProveedorDependenciasCommandValidator : AbstractValidator<ValidaCertificacionProveedorDependenciasCommand>
    {

        ValidaCertificacionProveedorDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
