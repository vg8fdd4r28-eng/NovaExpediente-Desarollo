using FluentValidation;

namespace NovaExpediente.Application.Colaboradores.Commands.ReasignarDMPorCliente
{
    public class ReasignarDMPorClienteCommandValidator : AbstractValidator<ReasignarDMPorClienteCommand>
    {


        public ReasignarDMPorClienteCommandValidator()
        {
            RuleFor(x => x.IdCliente).NotNull();
            RuleFor(x => x.IdDM).NotNull();
            RuleFor(x => x.IdDMOld).NotNull();
        }


    }
}
