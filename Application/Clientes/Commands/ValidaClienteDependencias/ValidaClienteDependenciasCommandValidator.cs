using FluentValidation;

namespace NovaExpediente.Application.Clientes.Commands.ValidaClienteDependencias
{
    class ValidaClienteDependenciasCommandValidator : AbstractValidator<ValidaClienteDependenciasCommand>
    {

        ValidaClienteDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
