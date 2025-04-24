using FluentValidation;

namespace NovaExpediente.Application.Tecnologias.Commands.ValidaTecnologiaDependencias
{
    class ValidaTecnologiaDependenciasCommandValidator : AbstractValidator<ValidaTecnologiaDependenciasCommand>
    {

        ValidaTecnologiaDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
