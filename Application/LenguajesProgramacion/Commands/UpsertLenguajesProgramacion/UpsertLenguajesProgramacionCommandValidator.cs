using FluentValidation;

namespace NovaExpediente.Application.LenguajesProgramacion.Commands.UpsertLenguajesProgramacion
{
    public class UpsertLenguajesProgramacionCommandValidator : AbstractValidator<UpsertLenguajesProgramacionCommand>
    {
        public UpsertLenguajesProgramacionCommandValidator()
        {

            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }
    }
}
