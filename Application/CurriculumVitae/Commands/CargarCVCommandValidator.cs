using FluentValidation;

namespace NovaExpediente.Application.CurriculumVitae.Commands
{
    public class CargarCVCommandValidator : AbstractValidator<CargarCVCommand>
    {
        public CargarCVCommandValidator()
        {
        }
    }
}
