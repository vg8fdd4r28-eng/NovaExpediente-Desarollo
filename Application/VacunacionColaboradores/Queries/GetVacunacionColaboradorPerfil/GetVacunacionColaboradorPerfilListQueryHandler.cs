

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.VacunasColaboradores.Queries.GetListaVacunasColaborador;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorPerfil
{
    public class GetVacunacionColaboradorPerfilListQueryHandler : IRequestHandler<GetVacunacionColaboradorPerfilListQuery, VacunacionColaboradorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacunacionColaboradorPerfilListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunacionColaboradorPerfilVm> Handle(GetVacunacionColaboradorPerfilListQuery request, CancellationToken cancellationToken)
        {
            var _VacunacionColaborador = await _context.VacunacionColaboradores
                .Where(c => c.IdColaborador == request.IdColaborador)
                .Include(c => c.VacunasColaborador.OrderBy(x => x.Fecha))
                .ProjectTo<VacunacionColaboradorPerfilDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new VacunacionColaboradorPerfilVm
            {
                VacunacionColaboradorPerfil = _VacunacionColaborador.FirstOrDefault()
            };

            if (vm.VacunacionColaboradorPerfil is null)
            {
                vm.VacunacionColaboradorPerfil = new VacunacionColaboradorPerfilDto
                {
                    TieneCertificado = false,
                    vacunasColaboradorPerfil = new List<VacunasColaboradorPerfilDto>()
                };
            }

            return vm;
        }


    }
}
