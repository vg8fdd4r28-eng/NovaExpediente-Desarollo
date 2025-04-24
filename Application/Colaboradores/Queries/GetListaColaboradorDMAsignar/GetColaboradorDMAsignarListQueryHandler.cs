using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDMAsignar
{
    public class GetColaboradorDMAsignarListQueryHandler : IRequestHandler<GetColaboradorDMAsignarListQuery, ColaboradorDMAsignarListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetColaboradorDMAsignarListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradorDMAsignarListVm> Handle(GetColaboradorDMAsignarListQuery request, CancellationToken cancellationToken)
        {
            //Se obtiene el id del delivery manager del colaborador actual para excluir de la lista

            var ColaboradoresDelivery = await _context.Colaboradores
             .Where(c => (c.ACTIVO == true && c.IDCOLABORADOR == request.Id))
             .OrderBy(c => c.NOMBRE)
             .ProjectTo<ColaboradorDMAsignarDto>(_mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);

            var ColaboradorDelivery = new ColaboradorDMAsignarVm
            {
                ColaboradorDM = ColaboradoresDelivery.FirstOrDefault()
            };

            int? IdDeliveryManager = ColaboradorDelivery.ColaboradorDM.IDDELIVERYMANAGER;


            var ColaboradoresDM = await _context.Colaboradores
                .Where(c => c.ESDELIVERYMANAGER == true && c.ACTIVO == true && c.IDCOLABORADOR != request.Id && c.IDCOLABORADOR != IdDeliveryManager)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<ColaboradorDMAsignarDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ColaboradorDMAsignarListVm
            {
                ColaboradoresDM = ColaboradoresDM,
                Count = ColaboradoresDM.Count
            };

            return vm;
        }


    }
}
