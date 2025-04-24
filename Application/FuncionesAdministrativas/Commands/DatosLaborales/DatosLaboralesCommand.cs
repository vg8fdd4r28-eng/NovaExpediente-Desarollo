using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FuncionesAdministrativas.Commands.DatosLaborales
{
    public class DatosLaboralesCommand : IRequest<string>
    {
        public string data { get; set; }


        public class DatosLaboralesCommandHandler : IRequestHandler<DatosLaboralesCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;
     

            public DatosLaboralesCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand)//, IEventBus eventBus) //, INotificationService notificationService)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
   
            }

            public async Task<string> Handle(DatosLaboralesCommand request, CancellationToken cancellationToken)
            {

                // se actualiza en la base de datos el colaborador
                string result = await _context.DatosLaborales(request.data);


                return result;
            }







        }
    }
}
