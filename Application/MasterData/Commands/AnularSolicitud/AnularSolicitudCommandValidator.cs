using FluentValidation;

namespace NovaExpediente.Application.MasterData.Commands.AnularSolicitud
{
    public class AnularSolicitudCommandValidator : AbstractValidator<AnularSolicitudCommand>
    {


        public AnularSolicitudCommandValidator()
        {
            RuleFor(x => x.Comentario).MaximumLength(1000).NotEmpty();
        }


    }
}
