using FluentValidation;

namespace NovaExpediente.Application.HablilidadesBlandas.Commands.UpsertHabilidadBlanda
{
    class UpsertHabilidadBlandaCommandValidator : AbstractValidator<UpsertHabilidadBlandaCommand>
    {
        public UpsertHabilidadBlandaCommandValidator()
        {

            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();

        }
    }
}
