using FluentValidation;

namespace NovaExpediente.Application.BPMS.Commands.UpsertBPM
{
    class UpsertBPMCommandValidator : AbstractValidator<UpsertBPMCommand>
    {
        public UpsertBPMCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Descripcion).MaximumLength(500);
            
        }
    }
}
