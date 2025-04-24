using FluentValidation;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Commands.UpsertSorteo
{
    public class UpsertSorteoCommandValidator : AbstractValidator<UpsertSorteoCommand>
    {
        public UpsertSorteoCommandValidator()
        {
            RuleFor(x => x.Data).NotEmpty();
        }
    }
}
