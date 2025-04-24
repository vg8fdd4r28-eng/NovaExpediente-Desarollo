using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FuncionesAdministrativas.Queries.GetBitacoraVacaciones
{
    public class GetBitacoraVacacionesQueryHandler : IRequestHandler<GetBitacoraVacacionesQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;
        public GetBitacoraVacacionesQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetBitacoraVacacionesQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.ConsultaBitacoraVacaciones();
            return list;
        }
    }
}
