using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Log.Commands.SaveLog
{
    public class SaveLogCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string Opcion { get; set; }
        public string NombreOpcion { get; set; }
        public string SubjectId { get; set; }
        public string Params { get; set; }

        public class SaveLogCommandHandler : IRequestHandler<SaveLogCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;

            public SaveLogCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
            }

            public async Task<int> Handle(SaveLogCommand request, CancellationToken cancellationToken)
            {

                // delega la persistencia de la solicitud a otra tarea en el pool de hilos de ejecución
                _fireForgetCommand.Execute(async db =>
                {
                    // Will receive its own scoped repository on the executing task
                    await db.PersistirBitacora(new Bitacora()
                    {
                        CORREO = request.Email,
                        OPCION = request.Opcion,
                        NOMBREOPCION = request.NombreOpcion,
                        NOMBREUSUARIO = request.NombreUsuario,
                        PARAMETROS = request.Params,
                        FECHAVISITA = DateTime.Now
                    });
                });


                return 1;
            }

        }
    }
}
