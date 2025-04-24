using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Actividades.Queries.GetDetalleActividad
{
    public class GetDetalleActividadQueryHandler : IRequestHandler<GetDetalleActividadQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;
        public GetDetalleActividadQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetDetalleActividadQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.ConsultaDetalleActividad(request.IdActividad);
            return list;
        }
    }
}
