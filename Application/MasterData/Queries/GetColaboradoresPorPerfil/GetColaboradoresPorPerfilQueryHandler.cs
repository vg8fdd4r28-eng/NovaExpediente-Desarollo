using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetColaboradoresPorPerfil
{
    public class GetColaboradoresPorPerfilQueryHandler : IRequestHandler<GetColaboradoresPorPerfilQuery, ColaboradoresPorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetColaboradoresPorPerfilQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradoresPorPerfilVm> Handle(GetColaboradoresPorPerfilQuery request, CancellationToken cancellationToken)
        {
            ColaboradoresPorPerfilVm vm = new ColaboradoresPorPerfilVm();
            List<ColaboradorPerfilDto> colaboradores = new List<ColaboradorPerfilDto>();

                 // si es Expediente.DM solo los colaboradores a cargo de REQUEST.IDCOLABORADOR
             if (request.Roles.Contains("Expediente.DM"))
            {
                colaboradores = await _context.Colaboradores
                                        .Where(c => c.IDDELIVERYMANAGER.HasValue &&
                                                    c.IDDELIVERYMANAGER.Value == request.IdColaborador &&
                                                    c.ACTIVO)
                                        .ProjectTo<ColaboradorPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }
            // si es Expediente.RRHH todos los colaboradores
            if (request.Roles.Contains("Expediente.RRHH"))
            {
                colaboradores = await _context.Colaboradores
                                        .Where(c => c.ACTIVO)
                                        .ProjectTo<ColaboradorPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }
       

            vm.Colaboradores = colaboradores;
            vm.Count = colaboradores.Count();

            return vm;
        }
    }
}
