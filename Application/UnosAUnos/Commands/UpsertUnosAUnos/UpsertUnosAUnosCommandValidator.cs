using FluentValidation;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Linq;

namespace NovaExpediente.Application.UnosAUnos.Commands.UpsertUnosAUnos
{
    public class UpsertUnosAUnosCommandValidator : AbstractValidator<UpsertUnosAUnosCommand>
    {
        private readonly INovaExpedienteDbContext _context;
        public UpsertUnosAUnosCommandValidator(INovaExpedienteDbContext context)
        {
            _context = context;
            RuleFor(x => x.data).NotEmpty();
            RuleFor(x => x).Must(ValidarRegistro).WithMessage("Ya existe un registro para el colaborador en el mismo mes y año");

        }

        private bool ValidarRegistro(UpsertUnosAUnosCommand request)
        {

            JsonUnoaUno item;
            DateTime FECHA_EJECUCION = DateTime.Today;

            item = JsonConvert.DeserializeObject<JsonUnoaUno>(request.data);
            var data = _context.UnoAUnos.Find(item.IDUNOAUNO);
            if (data != null)
            {
                FECHA_EJECUCION = data.FECHA_EJECUCION;
            }

            if (_context.UnoAUnos
                     .Any(w => w.IDCOLABORADOR == item.IDCOLABORADOR && w.ACTIVO && w.IDUNOAUNO != item.IDUNOAUNO &&
                                 w.FECHA_EJECUCION.Month == FECHA_EJECUCION.Month &&
                                 w.FECHA_EJECUCION.Year ==  FECHA_EJECUCION.Year))
            {

                return false;
            }
            return true;
        }

        public class JsonUnoaUno
        {
            public int? IDUNOAUNO { get; set; }
            public int IDCOLABORADOR { get; set; }
        }


    }
}
