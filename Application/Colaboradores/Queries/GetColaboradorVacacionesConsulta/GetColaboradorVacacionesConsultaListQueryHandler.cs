using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesConsulta
{
    public class GetColaboradorVacacionesConsultaListQueryHandler : IRequestHandler<GetColaboradorVacacionesConsultaListQuery, ColaboradorVacacionesConsultaVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;


         

        public GetColaboradorVacacionesConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradorVacacionesConsultaVm> Handle(GetColaboradorVacacionesConsultaListQuery request, CancellationToken cancellationToken)
        {
            ColaboradorVacacionesConsultaVm vm = new ColaboradorVacacionesConsultaVm();
            List<ColaboradorVacacionesConsultaDto> saldos = new List<ColaboradorVacacionesConsultaDto>();

          
                saldos = await _context.Colaboradores
                                      .Where(c => c.ACTIVO == true && c.SOLICITAVACACIONES  && (c.IDDELIVERYMANAGER == request.IdColaborador || c.IDCOLABORADOR == request.IdColaborador))
                                      .ProjectTo<ColaboradorVacacionesConsultaDto>(_mapper.ConfigurationProvider)
                                      .OrderBy(v => v.NombreCompleto)
                                      .ToListAsync(cancellationToken);

            vm.ColaboradorVacaciones = saldos;
            vm.Count = saldos.Count();

            return vm;
        }


    }
}
