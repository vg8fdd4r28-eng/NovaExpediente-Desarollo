using FluentValidation;

namespace NovaExpediente.Application.Clouds.Commands.ValidaCloudDependencias
{
    class ValidaCloudDependenciasCommandValidator : AbstractValidator<ValidaCloudDependenciasCommand>
    {

        ValidaCloudDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
