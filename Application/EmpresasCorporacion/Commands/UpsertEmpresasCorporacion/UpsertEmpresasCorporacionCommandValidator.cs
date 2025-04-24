using FluentValidation;

namespace NovaExpediente.Application.EmpresasCorporacion.Commands.UpsertEmpresasCorporacion
{
    public class UpsertEmpresasCorporacionCommandValidator : AbstractValidator<UpsertEmpresasCorporacionCommand>
    {
        public UpsertEmpresasCorporacionCommandValidator() {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
        }

    }
}
