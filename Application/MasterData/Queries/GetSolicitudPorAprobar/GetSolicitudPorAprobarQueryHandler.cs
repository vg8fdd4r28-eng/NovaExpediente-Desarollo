using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetSolicitudPorAprobar
{
    public class GetSolicitudPorAprobarQueryHandler : IRequestHandler<GetSolicitudPorAprobarQuery, SolicitudPorAprobarVm>
    {
        private readonly INovaExpedienteDbContext _context;

        public GetSolicitudPorAprobarQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }

        public async Task<SolicitudPorAprobarVm> Handle(GetSolicitudPorAprobarQuery request, CancellationToken cancellationToken)
        {
            SolicitudPorAprobarVm vm = new SolicitudPorAprobarVm();

            var solicitudes = await _context.SolicitudMasterData
                .Include(p => p.TipoSolicitud)
                .Include(p => p.Responsable)
                .Where(p => p.IDSOLICITUDMASTERDATA == request.IdSolicitud)
                .ToListAsync(cancellationToken);

            // solicitud existe?
            if (solicitudes.Count == 0)
            {
                vm.PuedeAprobar = false;
                vm.MensajeValidacion = "La solicitud no existe.";
            }
            else 
            {
                var solicitud = solicitudes.First();
                if (solicitud.Responsable.CORREO != request.Correo)
                {
                    vm.PuedeAprobar = false;
                    vm.MensajeValidacion = "Usted no es el responsable de aprobar la solicitud.";
                }
                else if (solicitud.ESTADOSOLICITUD != request.Estado)
                {
                    vm.PuedeAprobar = false;
                    vm.MensajeValidacion = "La solicitud ya fue atendida.";
                }
                else
                {
                    vm.PuedeAprobar = true;
                    vm.TipoSolicitud = solicitud.TipoSolicitud.NOMBRE;
                    vm.IdTipoSolicitud = solicitud.IDTIPOSOLICITUD;
                }
            }

            return vm;
        }
    }
}
