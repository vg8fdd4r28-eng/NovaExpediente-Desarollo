using FluentValidation;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;

namespace NovaExpediente.Application.FuncionesAdministrativas.Commands.DatosLaborales
{
    public class DatosLaboralesCommandValidator : AbstractValidator<DatosLaboralesCommand>
    {
        private readonly INovaExpedienteDbContext _context;
        public DatosLaboralesCommandValidator(INovaExpedienteDbContext context)
        {
            _context = context;
          //  RuleFor(x => JsonConvert.DeserializeObject<DatosLaboralesData>(x.data).identificacion).Must(DuplicidadIdentificacion).WithMessage("La identificación ingresada ya existe.");
            //RuleFor(x => x).Must(IdentificacionInvalida).WithMessage("La identificación ingresada no existe.");
        }

        private bool IdentificacionInvalida(DatosLaboralesCommand solicitud)
        {

            DatosLaboralesData doc;

            doc = JsonConvert.DeserializeObject<DatosLaboralesData>(solicitud.data);



            var colaborador = _context.Colaboradores
                                         .Where(s => s.IDENTIFICACION == doc.identificacion)
                                         .FirstOrDefault();
            if (colaborador == null)
            {
                return true;
            }
            return false;




        }


        public class DatosLaboralesData
        {
            public string identificacion { get; set; }


        }

    }
}
