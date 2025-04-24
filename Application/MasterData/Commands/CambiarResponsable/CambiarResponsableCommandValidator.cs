using FluentValidation;

namespace NovaExpediente.Application.MasterData.Commands.CambiarResponsable
{
    public class CambiarResponsableCommandValidator : AbstractValidator<CambiarResponsableCommand>
    {


        public CambiarResponsableCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.IdResponsable).NotNull();
            //RuleFor(x => x.Address).MaximumLength(60);
            //RuleFor(x => x.City).MaximumLength(15);
            // RuleFor(x => x.IdDM).MaximumLength(100).NotEmpty();
            //RuleFor(x => x.ContactName).MaximumLength(30);
            //RuleFor(x => x.ContactTitle).MaximumLength(30);
            //RuleFor(x => x.Country).MaximumLength(15);
            //RuleFor(x => x.Fax).MaximumLength(24);
            //RuleFor(x => x.Phone).MaximumLength(24);
            //RuleFor(x => x.PostalCode).MaximumLength(10).NotEmpty();
            //RuleFor(x => x.Region).MaximumLength(15);
        }


    }
}
