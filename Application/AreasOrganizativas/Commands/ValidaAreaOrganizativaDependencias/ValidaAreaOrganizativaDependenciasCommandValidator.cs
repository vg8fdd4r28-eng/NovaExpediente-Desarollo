using FluentValidation;

namespace NovaExpediente.Application.AreasOrganizativas.Commands.ValidaAreaOrganizativaDependencias
{
    class ValidaAreaOrganizativaDependenciasCommandValidator : AbstractValidator<ValidaAreaOrganizativaDependenciasCommand>
    {

        ValidaAreaOrganizativaDependenciasCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
