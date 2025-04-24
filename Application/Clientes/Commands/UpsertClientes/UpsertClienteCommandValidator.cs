using FluentValidation;

namespace NovaExpediente.Application.Clientes.Commands.UpsertClientes
{
    class UpsertClienteCommandValidator : AbstractValidator<UpsertClienteCommand>
    {

        UpsertClienteCommandValidator()
        {
            RuleFor(x => x.IDSECTOR).NotNull();
            RuleFor(x => x.IDPAIS).NotNull();
            RuleFor(x => x.CODIGOCLIENTE).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CEDULAJURIDICA).NotEmpty().MaximumLength(25);
            RuleFor(x => x.NOMBRE).NotEmpty().MaximumLength(25);
            RuleFor(x => x.DESCRIPCION).NotEmpty().MaximumLength(8000);
            RuleFor(x => x.CORREO).NotEmpty().MaximumLength(100);
            RuleFor(x => x.TELEFONO).NotEmpty().MaximumLength(9);
            RuleFor(x => x.ACTIVO).NotNull();
            RuleFor(x => x.TIPOCLIENTE).NotEmpty().MaximumLength(1);


        }
    }
}
