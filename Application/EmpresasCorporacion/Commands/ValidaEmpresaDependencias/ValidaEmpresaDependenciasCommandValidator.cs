using FluentValidation;

namespace NovaExpediente.Application.EmpresasCorporacion.Commands.ValidaEmpresaDependencias
{
    class ValidaEmpresaDependenciasCommandValidator : AbstractValidator<ValidaEmpresaDependenciasCommand>
    {

        ValidaEmpresaDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
