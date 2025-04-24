using FluentValidation;
using Newtonsoft.Json;
using NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaboradorResumen;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Linq;

namespace NovaExpediente.Application.FuncionesAdministrativas.Commands.RegistroColaborador
{
    public class RegistroColaboradorCommandValidator : AbstractValidator<RegistroColaboradorCommand>
    {
        private readonly INovaExpedienteDbContext _context;
        public RegistroColaboradorCommandValidator(INovaExpedienteDbContext context)
        {
            _context = context;
          //  RuleFor(x => JsonConvert.DeserializeObject<RegistroColaboradorData>(x.data).identificacion).Must(DuplicidadIdentificacion).WithMessage("La identificación ingresada ya existe.");
            RuleFor(x => x).Must(DuplicidadIdentificacion).WithMessage("La identificación ingresada ya existe.");
        }

        private bool DuplicidadIdentificacion(RegistroColaboradorCommand solicitud)
        {

            RegistroColaboradorData doc;

            doc = JsonConvert.DeserializeObject<RegistroColaboradorData>(solicitud.data);



            var colaborador = _context.Colaboradores
                                         .Where(s => s.IDENTIFICACION == doc.identificacion)
                                         .FirstOrDefault();
            if (colaborador != null)
            {
                return false;
            }
            return true;




        }


        public class RegistroColaboradorData
        {
            public string identificacion { get; set; }


        }

    }
}
