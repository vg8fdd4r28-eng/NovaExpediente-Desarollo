using FluentValidation;

namespace NovaExpediente.Application.ProveedorCerficaciones.Commands.ValidaProveedorDependencias
{
    class ValidaProveedorDependenciasCommandValidator : AbstractValidator<ValidaProveedorDependenciasCommand>
    {

        ValidaProveedorDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
