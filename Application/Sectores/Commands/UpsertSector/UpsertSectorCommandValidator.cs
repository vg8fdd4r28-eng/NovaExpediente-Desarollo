using FluentValidation;

namespace NovaExpediente.Application.Sectores.Commands.UpsertSector
{
    class UpsertSectorCommandValidator : AbstractValidator <UpsertSectorCommand>
    {
        public UpsertSectorCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }
    }
}
