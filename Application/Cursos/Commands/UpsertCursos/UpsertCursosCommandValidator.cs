using FluentValidation;

namespace NovaExpediente.Application.Cursos.Commands.UpsertCursos
{
    public class UpsertCursosCommandValidator : AbstractValidator<UpsertCursosCommand>
    {

        public UpsertCursosCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }

    }
}
