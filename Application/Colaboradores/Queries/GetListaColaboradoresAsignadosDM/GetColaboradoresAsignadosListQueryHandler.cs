using AutoMapper;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresAsignadosDM
{
    public class GetColaboradoresAsignadosListQueryHandler : IRequestHandler<GetColaboradoresAsignadosListQuery, ColaboradoresAsignadosDMListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;



        public GetColaboradoresAsignadosListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradoresAsignadosDMListVm> Handle(GetColaboradoresAsignadosListQuery request, CancellationToken cancellationToken)
        {


           
            var lista = await _context.ConsultaColaboradoresAsignadosDM(request.IdDM);

            var vm = new ColaboradoresAsignadosDMListVm
            {
                Colaboradores = lista,
                Count = lista.Count()
            };

            return vm;
        }        
    }
}
