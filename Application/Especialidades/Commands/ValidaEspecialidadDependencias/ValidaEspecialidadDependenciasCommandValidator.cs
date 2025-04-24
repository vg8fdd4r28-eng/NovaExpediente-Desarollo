using FluentValidation;

namespace NovaExpediente.Application.Especialidades.Commands.ValidaEspecialidadDependencias
{
    class ValidaEspecialidadDependenciasCommandValidator : AbstractValidator<ValidaEspecialidadDependenciasCommand>
    {

        ValidaEspecialidadDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
