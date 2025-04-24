using FluentValidation;

namespace NovaExpediente.Application.Infraestructuras.Commands.ValidaInfraestructuraDependencias
{
    class ValidaInfraestructuraDependenciasCommandValidator : AbstractValidator<ValidaInfraestructuraDependenciasCommand>
    {

        ValidaInfraestructuraDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
