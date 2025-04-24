using FluentValidation;

namespace NovaExpediente.Application.CertificacionesProveedor.Commands.ValidaProveedorCertificacionDependencias
{
    class ValidaProveedorCertificacionDependenciasCommandValidator : AbstractValidator<ValidaProveedorCertificacionDependenciasCommand>
    {

        ValidaProveedorCertificacionDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
