using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetColaboradoresAllPorPerfil
{
    public class GetColaboradoresAllPorPerfilQueryHandler : IRequestHandler<GetColaboradoresAllPorPerfilQuery, ColaboradoresAllPorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetColaboradoresAllPorPerfilQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradoresAllPorPerfilVm> Handle(GetColaboradoresAllPorPerfilQuery request, CancellationToken cancellationToken)
        {
            ColaboradoresAllPorPerfilVm vm = new ColaboradoresAllPorPerfilVm();
            List<ColaboradorAllPerfilDto> colaboradores = new List<ColaboradorAllPerfilDto>();

            //  si es Expediente.DM solo los colaboradores a cargo de REQUEST.IDCOLABORADOR
            if (request.Roles.Contains("Expediente.DM"))
            {
                colaboradores = await _context.Colaboradores
                                       .Where(c => c.IDDELIVERYMANAGER.HasValue &&
                                                   c.IDDELIVERYMANAGER.Value == request.IdColaborador)
                                       .ProjectTo<ColaboradorAllPerfilDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken);
            }
            // si es Expediente.RRHH todos los colaboradores
            if (request.Roles.Contains("Expediente.RRHH"))
            {
                colaboradores = await _context.Colaboradores
                                         .ProjectTo<ColaboradorAllPerfilDto>(_mapper.ConfigurationProvider)
                                         .ToListAsync(cancellationToken);
            }

            vm.Colaboradores = colaboradores;
            vm.Count = colaboradores.Count();

            return vm;
        }
    }
}
