using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresConsulta
{
    class GetColaboradoresConsultaListQueryHandler : IRequestHandler<GetColaboradoresConsultaListQuery, ColaboradoresConsultaListVm>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetColaboradoresConsultaListQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<ColaboradoresConsultaListVm> Handle(GetColaboradoresConsultaListQuery request, CancellationToken cancellationToken)
        {

            var colaboradores = await _context.ConsultaCriterios(request.Query);

            var vm = new ColaboradoresConsultaListVm
            {
                Colaboradores = colaboradores,
                Cantidad = colaboradores.Count()
             };

            return vm;
        }
    }
}
