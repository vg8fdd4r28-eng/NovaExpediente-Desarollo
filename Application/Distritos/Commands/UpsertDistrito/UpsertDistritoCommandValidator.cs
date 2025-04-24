using FluentValidation;

namespace NovaExpediente.Application.Distritos.Commands.UpsertDistrito
{
    class UpsertDistritoCommandValidator : AbstractValidator<UpsertDistritoCommand>
    {

        public UpsertDistritoCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
            RuleFor(x => x.IdCanton).NotNull();
        }
    }
}
