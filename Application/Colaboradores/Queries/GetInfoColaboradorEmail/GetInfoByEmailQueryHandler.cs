using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.MasterData.Queries.GetListaSolicitudes;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetInfoColaboradorEmail
{
    public class GetInfoByEmailQueryHandler : IRequestHandler<GetInfoByEmailQuery, InfoColaboradorVM>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;


        public GetInfoByEmailQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoColaboradorVM> Handle(GetInfoByEmailQuery request, CancellationToken cancellationToken)
        {
            var colaborador = await _context.Colaboradores
                .Where(c => c.CORREO == request.Email)
                .ProjectTo<InfoColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new InfoColaboradorVM
            {
                InfoColaborador = colaborador.FirstOrDefault()
            };

            if (!vm.InfoColaborador.Activo)
            {
                throw new Exception("El colaborador se encuentra inactivo");
            }

            if (vm.InfoColaborador != null)
            {
                // obtener la cantidad de solicitudes de MI BUZON
                vm.InfoColaborador.ContadorInbox = await ObtenerCantidadSolicitudes(vm.InfoColaborador.Id, cancellationToken);
                // obtener la cantidad de solicitudes de pendientes de atender

                vm.InfoColaborador.ContadorAdminInbox = await ObtenerCantidadSolicitudesAdmin(vm.InfoColaborador.Id, cancellationToken); 
            }
            return vm;
        }

        private async Task<int> ObtenerCantidadSolicitudes(int id, CancellationToken cancellationToken)
        {
            ListaSolicitudesVm solicitudes = null;
            GetListaSolicitudesBuzonQueryHandler solQueryHandler = new GetListaSolicitudesBuzonQueryHandler(_context, _mapper);
            GetListaSolicitudesBuzonQuery solQuery = new GetListaSolicitudesBuzonQuery()
            {
                IdUsuario = id,
                Tipo = "MiBuzon"
            };

            solicitudes = await solQueryHandler.Handle(solQuery, cancellationToken);
            return solicitudes.count;
        }

        private async Task<int> ObtenerCantidadSolicitudesAdmin(int id, CancellationToken cancellationToken)
        {
            ListaSolicitudesVm solicitudes = null;
            GetListaSolicitudesBuzonQueryHandler solQueryHandler = new GetListaSolicitudesBuzonQueryHandler(_context, _mapper);
            GetListaSolicitudesBuzonQuery solQuery = new GetListaSolicitudesBuzonQuery()
            {
                IdUsuario = id,
                Tipo = "MiBuzonAdmin"
            };

            solicitudes = await solQueryHandler.Handle(solQuery, cancellationToken);
            return solicitudes.count;
        }

        
    }
}
