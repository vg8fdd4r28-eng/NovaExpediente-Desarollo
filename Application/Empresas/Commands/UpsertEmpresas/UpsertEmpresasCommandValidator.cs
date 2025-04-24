using FluentValidation;

namespace NovaExpediente.Application.Empresas.Commands.UpsertEmpresas
{
    public class UpsertEmpresasCommandValidator : AbstractValidator<UpsertEmpresasCommand>
    {
        public UpsertEmpresasCommandValidator() {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
            RuleFor(x => x.cedulaJuridica).MaximumLength(100).NotEmpty();
        }

    }
}
