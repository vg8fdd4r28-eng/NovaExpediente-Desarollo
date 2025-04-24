using FluentValidation;

namespace NovaExpediente.Application.AplicacionMovil.Convenios.Commands.UpsertConvenios
{
    public class UpsertConveniosCommandValidator : AbstractValidator<UpsertConveniosCommand>
    {

        public UpsertConveniosCommandValidator()
        {
            RuleFor(x => x.data).NotEmpty();
        }



 

    }
}
