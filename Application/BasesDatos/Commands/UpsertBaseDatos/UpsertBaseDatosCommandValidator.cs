using FluentValidation;

namespace NovaExpediente.Application.BasesDatos.Commands.UpsertBaseDatos
{
    class UpsertBaseDatosCommandValidator : AbstractValidator<UpsertBaseDatosCommand>
    {
        public UpsertBaseDatosCommandValidator()
        {
           
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();

        }
    }
}
