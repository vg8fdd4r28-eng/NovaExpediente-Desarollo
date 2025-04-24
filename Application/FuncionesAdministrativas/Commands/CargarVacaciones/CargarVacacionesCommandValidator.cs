using FluentValidation;
using NovaExpediente.Application.Common.Interfaces;

namespace NovaExpediente.Application.FuncionesAdministrativas.Commands.CargarVacaciones
{
    public class CargarVacacionesCommandValidator : AbstractValidator<CargarVacacionesCommand>
    {
        private readonly INovaExpedienteDbContext _context;
        public CargarVacacionesCommandValidator(INovaExpedienteDbContext context)
        {
            _context = context;
        RuleFor(x => x).Must(DuplicidadIdentificacion).WithMessage("La identificación ingresada ya existe.");
        }

        private bool DuplicidadIdentificacion(CargarVacacionesCommand solicitud)
        {

            

            //var colaborador = _context.Colaboradores
            //                             .Where(s => s.IDENTIFICACION == solicitud.dataList[0].IDENTIFICACION)
            //                             .FirstOrDefault();
            //if (colaborador != null)
            //{
            //    return false;
            //}
            return true;




        }


     

    }
}
