using FluentValidation;

namespace NovaExpediente.Application.AreasOrganizativas.Commands.UpsertAreaOrganizativa
{
    public class UpsertAreaOrganizativaCommandValidator: AbstractValidator<UpsertAreaOrganizativaCommand>
    {
        public UpsertAreaOrganizativaCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }

    }
}
