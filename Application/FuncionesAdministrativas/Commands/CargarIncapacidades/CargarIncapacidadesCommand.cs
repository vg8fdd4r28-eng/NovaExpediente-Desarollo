using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FuncionesAdministrativas.Commands.CargarIncapacidades
{
    public class CargarIncapacidadesCommand : IRequest<string>
    {
        public List<IncapacidadArchivo> dataList { get; set; }
        public string nombreArchivo { get; set; }
        public string Json { get; set; }
        public int cantRegistros { get; set; }
        public int idUsuarioCrea { get; set; }




        public class CargarIncapacidadesCommandHandler : IRequestHandler<CargarIncapacidadesCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;

 

            public CargarIncapacidadesCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand)
                                                                                                                                   
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;

            }

            public async Task<string> Handle(CargarIncapacidadesCommand request, CancellationToken cancellationToken)
            {

                string result = "";

                string data = request.Json;

                result = await _context.CargarIncapacidad(data.Replace(";", "").Replace(@"\", "").Replace("r", "").Trim(), request.nombreArchivo, request.cantRegistros, request.idUsuarioCrea);



                return result;
            }







        }
    }

    public class IncapacidadArchivo
    {

        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string numeroboleta { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public int cantdias { get; set; }
        public string tipo { get; set; }



    }

}
