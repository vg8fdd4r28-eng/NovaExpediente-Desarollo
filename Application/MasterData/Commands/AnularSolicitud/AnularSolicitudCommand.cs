using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Domain.Entities;
using NovaExpediente.Application.Common.Interfaces;



namespace NovaExpediente.Application.MasterData.Commands.AnularSolicitud
{
    public class AnularSolicitudCommand : IRequest<int>
    {

        public int Id { get; set; }
        public int IdColaborador { get; set; }

        public string Comentario { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<AnularSolicitudCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(AnularSolicitudCommand request, CancellationToken cancellationToken)
            {
                SolicitudMasterData entity;
                SolicitudComentario entityComentario;


                entity = await _context.SolicitudMasterData.FindAsync(request.Id);


                if (entity != null)
                {
                    entity.ESTADOSOLICITUD = "AN";


                    entityComentario = new SolicitudComentario();

                    _context.SolicitudComentario.Add(entityComentario);

                    entityComentario.COMENTARIO = request.Comentario;
                    entityComentario.IDUSUARIOCREA = request.IdColaborador;
                    entityComentario.ACCION = "Anular";
                    entityComentario.IDSOLICITUDMASTERDATA = entity.IDSOLICITUDMASTERDATA;


                    await _context.SaveChangesAsync(cancellationToken);

                    return entity.IDSOLICITUDMASTERDATA;
                }

                else
                {
                    return 0;
                }

            }
        }

    }
}
