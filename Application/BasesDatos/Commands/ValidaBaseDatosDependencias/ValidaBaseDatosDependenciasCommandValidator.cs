using FluentValidation;

namespace NovaExpediente.Application.BasesDatos.Commands.ValidaBaseDatosDependencias
{
    class ValidaBaseDatosDependenciasCommandValidator : AbstractValidator<ValidaBaseDatosDependenciasCommand>
    {

        ValidaBaseDatosDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
