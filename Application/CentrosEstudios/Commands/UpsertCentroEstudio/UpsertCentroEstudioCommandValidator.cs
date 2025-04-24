using FluentValidation;

namespace NovaExpediente.Application.CentrosEstudios.Commands.UpsertCentroEstudio
{
    public class UpsertCentroEstudioCommandValidator : AbstractValidator<UpsertCentroEstudioCommand>
    {
        public UpsertCentroEstudioCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }
    }
}
