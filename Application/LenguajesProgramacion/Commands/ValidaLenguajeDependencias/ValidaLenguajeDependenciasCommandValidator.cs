using FluentValidation;

namespace NovaExpediente.Application.LenguajesProgramacion.Commands.ValidaLenguajeDependencias
{
    class ValidaLenguajeDependenciasCommandValidator : AbstractValidator<ValidaLenguajeDependenciasCommand>
    {

        ValidaLenguajeDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
