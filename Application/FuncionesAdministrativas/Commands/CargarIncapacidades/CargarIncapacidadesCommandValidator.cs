using FluentValidation;
using Newtonsoft.Json;
using NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaboradorResumen;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Linq;

namespace NovaExpediente.Application.FuncionesAdministrativas.Commands.CargarIncapacidades
{
    public class CargarIncapacidadesCommandValidator : AbstractValidator<CargarIncapacidadesCommand>
    {
        private readonly INovaExpedienteDbContext _context;
        public CargarIncapacidadesCommandValidator(INovaExpedienteDbContext context)
        {
            _context = context;
        RuleFor(x => x).Must(DuplicidadIdentificacion).WithMessage("La identificación ingresada ya existe.");
        }

        private bool DuplicidadIdentificacion(CargarIncapacidadesCommand solicitud)
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
