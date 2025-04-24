using FluentValidation;

namespace NovaExpediente.Application.Puestos.Commands.UpsertPuesto
{
    public class UpsertPuestoCommandValidator : AbstractValidator<UpsertPuestoCommand>
    { 
        public UpsertPuestoCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }
    }
}
