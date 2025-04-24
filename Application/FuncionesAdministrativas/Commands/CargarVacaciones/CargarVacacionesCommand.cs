using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FuncionesAdministrativas.Commands.CargarVacaciones
{
    public class CargarVacacionesCommand : IRequest<string>
    {
        public List<VacacionesArchivo> dataList { get; set; }
        public string nombreArchivo { get; set; }
        public string Json { get; set; }
        public int cantRegistros { get; set; }
        public int idUsuarioCrea { get; set; }




        public class CargarVacacionesCommandHandler : IRequestHandler<CargarVacacionesCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;

 

            public CargarVacacionesCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand)
                                                                                                                                   
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;

            }

            public async Task<string> Handle(CargarVacacionesCommand request, CancellationToken cancellationToken)
            {

                string result = "";

                string data = request.Json;

                result = await _context.CargarVacaciones(data.Replace(";", "").Replace(@"\r", "").Trim(), request.nombreArchivo, request.cantRegistros, request.idUsuarioCrea);



                return result;
            }







        }
    }

    public class VacacionesArchivo
    {

        public string identificacion { get; set; }
        public string nombre { get; set; }
        public double saldo { get; set; }


    }

}
