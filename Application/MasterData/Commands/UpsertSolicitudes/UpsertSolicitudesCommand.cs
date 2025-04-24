using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.MasterData.Commands.UpsertSolicitudes
{
    public class UpsertSolicitudesCommand : IRequest<int>
    {
        public int? IdSolicitud { get; set; }
        public int IdTipoSolicitud { get; set; }

        public string CodigoEstado { get; set; }
        public int IdAutor { get; set; }

        public int? IdColaborador { get; set; }
        public int IdResponsable { get; set; }
        public string DocSolicitud { get; set; }        

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertSolicitudesCommand, int>
        {            
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertSolicitudesCommand request, CancellationToken cancellationToken)
            {
                SolicitudMasterData entity;

                if (request.IdSolicitud.HasValue && request.IdSolicitud.Value != 0)
                {
                    entity = await _context.SolicitudMasterData.FindAsync(request.IdSolicitud.Value);
                }
                else
                {
                    entity = new SolicitudMasterData();

                    _context.SolicitudMasterData.Add(entity);

                    entity.IDTIPOSOLICITUD = request.IdTipoSolicitud;
                    entity.IDUSUARIOCREA = request.IdAutor;
                }

                if (request.CodigoEstado == "RE")
                {
                    entity.IDCOLABORADOR = request.IdColaborador;
                }

           
                entity.IDUSUARIORESPONSABLE = request.IdResponsable;
                entity.ESTADOSOLICITUD = request.CodigoEstado;
                entity.DOCSOLICITUD = request.DocSolicitud;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDSOLICITUDMASTERDATA;
            }
        }
    }
}
