using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Noticias.Queries.GetDetalleNoticia
{
    public class GetDetalleNoticiaQueryHandler : IRequestHandler<GetDetalleNoticiaQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;
        public GetDetalleNoticiaQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetDetalleNoticiaQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.ConsultaDetalleNoticia(request.IdNoticia);
            return list;
        }
    }
}
