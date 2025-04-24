using FluentValidation;

namespace NovaExpediente.Application.Buses.Commands.UpsertBus
{
    class UpsertBusCommandValidator : AbstractValidator<UpsertBusCommand>
    {

        public UpsertBusCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Descripcion).MaximumLength(500);
        }
    }
}
