using FluentValidation;

namespace NovaExpediente.Application.Informes.Commands.InsertInformes
{
    public class InsertInformesCommandValidator : AbstractValidator<InsertInformesCommand>
    {
        public InsertInformesCommandValidator() => RuleFor(x => x.UserId).NotEmpty();
    }
}
