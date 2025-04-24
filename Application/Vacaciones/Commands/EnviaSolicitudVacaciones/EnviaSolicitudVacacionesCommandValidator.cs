using FluentValidation;
using System.Linq;

namespace NovaExpediente.Application.Vacaciones.Commands.EnviaSolicitudVacaciones
{
    public class EnviaSolicitudVacacionesCommandValidator : AbstractValidator<EnviaSolicitudVacacionesCommand>
    {
        public EnviaSolicitudVacacionesCommandValidator()
        {
            RuleFor(x => x.cantDias).NotNull().GreaterThan(0).WithMessage("La cantidad de días debe ser mayor a 0.");
            RuleFor(x => x.idColaborador).NotNull().GreaterThan(0).WithMessage("El id del colaborador es un valor requerido.");
            RuleFor(x => x.fechaInicio).NotNull().LessThanOrEqualTo(x => x.fechaFinal).WithMessage("La fecha de inicio debe ser menor o igual a la final.");
            RuleFor(x => x.fechaFinal).NotNull().GreaterThanOrEqualTo(x => x.fechaInicio).WithMessage("La fecha final debe ser mayor o igual a la de inicio.");
            RuleFor(x => x.tipoSolicitud).Must(x => new[] { "Vacaciones", "Permisos", "Incapacidades" }.Contains(x)).WithMessage("El valor del tipo de la solicitud debe ser (Vacaciones/Permisos/Incapacidades).");
        }
    }
}
