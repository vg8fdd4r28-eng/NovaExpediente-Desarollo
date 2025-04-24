using FluentValidation;

namespace NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Commands.UpsertNoticiasCategorias
{
    public class UpsertNoticiasCategoriasCommandValidator : AbstractValidator<UpsertNoticiasCategoriasCommand>
    {
        public UpsertNoticiasCategoriasCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Descripcion).MaximumLength(100).NotEmpty();
        }
    }
}
