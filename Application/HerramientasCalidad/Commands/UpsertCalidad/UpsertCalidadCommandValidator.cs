using FluentValidation;
namespace NovaExpediente.Application.HerramientasCalidad.Commands.UpsertCalidad
{
    public class UpsertCalidadCommandValidator :AbstractValidator<UpsertCalidadCommand>
    {
        public UpsertCalidadCommandValidator()
        {
         
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
           
        }
    }
}
