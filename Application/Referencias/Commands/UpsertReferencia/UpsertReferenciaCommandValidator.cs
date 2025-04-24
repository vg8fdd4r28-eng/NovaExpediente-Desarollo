using FluentValidation;

namespace NovaExpediente.Application.Referencias.Commands.UpsertReferencia
{
    class UpsertReferenciaCommandValidator : AbstractValidator<UpsertReferenciaCommand>
    {
        public UpsertReferenciaCommandValidator()
        {
            RuleFor(x => x.IDCLIENTE).GreaterThan(0).NotNull();
            RuleFor(x => x.NOMBRE).MaximumLength(100).NotEmpty();
            RuleFor(x => x.IDENTIFICACION).MaximumLength(20).NotEmpty();
            RuleFor(x => x.CORREO).MaximumLength(100).NotEmpty();
            RuleFor(x => x.TELEFONO).MaximumLength(20).NotEmpty();
            RuleFor(x => x.EXTENSION).MaximumLength(10).NotEmpty();
            RuleFor(x => x.CELULAR).MaximumLength(20).NotEmpty();
            RuleFor(x => x.PUESTO).MaximumLength(100).NotEmpty();
            RuleFor(x => x.ACTIVO).NotNull();

        }
    }
}
