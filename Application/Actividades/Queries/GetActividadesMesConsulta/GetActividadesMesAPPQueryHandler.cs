using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Actividades.Queries.GetActividadesMesConsulta
{
    public class GetActividadesMesAPPQueryHandler : IRequestHandler<GetActividadesMesAPPQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;

        public GetActividadesMesAPPQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetActividadesMesAPPQuery request, CancellationToken cancellationToken)
        {
            var lista = await _context.ConsultaActividadesMesAPP(request.Mes, request.Anho, request.IdIdioma);
            return lista;
        }
    }
}