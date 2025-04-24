using FluentValidation;

namespace NovaExpediente.Application.NivelesIdiomas.Commands.UpsertNivelesIdiomas
{
    class UpsertNivelesIdiomasCommandValidator :AbstractValidator<UpsertNivelesIdiomasCommand>
    {
        public UpsertNivelesIdiomasCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Descripcion).MaximumLength(500);
        }
    }
}
