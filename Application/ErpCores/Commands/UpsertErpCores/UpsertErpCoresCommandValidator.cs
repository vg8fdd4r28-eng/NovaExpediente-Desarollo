using FluentValidation;

namespace NovaExpediente.Application.ErpCores.Commands.UpsertErpCores
{
    class UpsertErpCoresCommandValidator : AbstractValidator<UpsertErpCoresCommand>
    {

        UpsertErpCoresCommandValidator()
        {
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Descripcion).MaximumLength(500);
            RuleFor(x => x.Tecnologia).MaximumLength(100);
            
        }
    }
}
