
using FluentValidation;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Entidad.Commands.UpsertEntidad;
using System.Linq;

namespace NovaExpediente.Application.VacunasColaboradores.Commands.UpsertVacunasColaborador
{
    public class UpsertVacunasColaboradorCommandValidator : AbstractValidator<UpsertVacunasColaboradorCommand>
    {
        private readonly INovaExpedienteDbContext _context;

        public UpsertVacunasColaboradorCommandValidator(INovaExpedienteDbContext context)
        {
            _context = context;
            //RuleFor(x => x.Id).NotNull().GreaterThan(0);
            //RuleFor(x => x.Descripcion).MaximumLength(8000);
        }
    }


}