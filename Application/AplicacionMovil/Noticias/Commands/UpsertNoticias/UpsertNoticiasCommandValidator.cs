using FluentValidation;

namespace NovaExpediente.Application.AplicacionMovil.Noticias.Commands.UpsertNoticias
{
    public class UpsertNoticiasCommandValidator : AbstractValidator<UpsertNoticiasCommand>
    {

        public UpsertNoticiasCommandValidator()
        {
            RuleFor(x => x.data).NotEmpty();
        }



 

    }
}
