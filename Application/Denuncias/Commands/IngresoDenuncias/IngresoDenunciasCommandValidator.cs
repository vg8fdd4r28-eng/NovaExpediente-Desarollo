using FluentValidation;

namespace NovaExpediente.Application.Denuncias.Commands.IngresoDenuncias
{
    public class IngresoDenunciasCommandValidator : AbstractValidator<IngresoDenunciasCommand>
    {

        public IngresoDenunciasCommandValidator()
        {
            RuleFor(x => x.data).NotEmpty();
        }



 

    }
}
