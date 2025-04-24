using FluentValidation;

namespace NovaExpediente.Application.TiposCore.Commands.UpsertTipoCore
{
    class UpsertTipoCoreCommandValidator :AbstractValidator<UpsertTipoCoreCommand>
    {
        public UpsertTipoCoreCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }
    }
}
