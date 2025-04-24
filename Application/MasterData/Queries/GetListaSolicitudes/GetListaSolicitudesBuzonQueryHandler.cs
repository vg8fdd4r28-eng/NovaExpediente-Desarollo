using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Parametros.Queries.GetParametros;
using NovaExpediente.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetListaSolicitudes
{
    public class GetListaSolicitudesBuzonQueryHandler : IRequestHandler<GetListaSolicitudesBuzonQuery, ListaSolicitudesVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;
        private const string INBOX = "MiBuzon";
        private const string INBOXDM = "MiBuzonDM";
        private const string OUTBOX = "Tramitadas";
        private const string TRACKING = "Tracking";
        private const string HISTORY = "History";
        private const string CONSULTANTS = "Consultants";
        private const string INBOXADMIN = "MiBuzonAdmin";
        private const string INBOXADMINDM = "MiBuzonAdminDM";
        private const string OUTBOXADMIN = "TramitadasAdmin";
        private const string TRACKINGADMIN = "TrackingAdmin";


        private const string CAT_MASTERDATA = "MD";
        private const string CAT_ADMINISTRATIVE = "AD";

        List<string> estadosTracking = new List<string>()
                {
                    "EV", "CO", "EA"
                };
        List<string> estadosTrackingAdmin = new List<string>()
                {
                    "EP", "EPV", "CO","EA"
                };

        public GetListaSolicitudesBuzonQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaSolicitudesVm> Handle(GetListaSolicitudesBuzonQuery request, CancellationToken cancellationToken)
        {
            List<SolicitudDto> solicitudes;

            if (request.Tipo.Equals(INBOX))
            {
                // solo se obtienen las solicitudes que no son estados finales
                List<string> estadosInbox = new List<string>()
                {
                    "RE", "EV", "CO", "EA"
                };

                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => p.IDUSUARIORESPONSABLE == request.IdUsuario
                                                && estadosInbox.Contains(p.ESTADOSOLICITUD)
                                                && p.TipoSolicitud.CATEGORIA == CAT_MASTERDATA)
                                    .OrderBy(p => p.FECHACREACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
            else if (request.Tipo.Equals(INBOXDM))
            {
                // solo se obtienen las solicitudes que no son estados finales
                List<string> estadosInbox = new List<string>()
                {
                    "RE", "EV", "CO", "EA"
                };

                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => p.IDUSUARIORESPONSABLE == request.IdUsuario
                                                && estadosInbox.Contains(p.ESTADOSOLICITUD)
                                                && p.TipoSolicitud.CATEGORIA == CAT_MASTERDATA)
                                    .OrderBy(p => p.FECHACREACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);



                List<Colaborador> listColaboresDM = new List<Colaborador>();

                listColaboresDM = await _context.Colaboradores
                                .Where(p => p.IDDELIVERYMANAGER == request.IdUsuario).ToListAsync(cancellationToken);

                List<int> listIDColaboresDM = new List<int>();

                listIDColaboresDM = listColaboresDM.Select(p => p.IDCOLABORADOR).ToList();

                List<SolicitudDto> solicitudes2;

                solicitudes2 = await _context.SolicitudMasterData
                                    .Where(p => listIDColaboresDM.Contains((int)p.IDCOLABORADOR)
                                    //.Where(p => listIDColaboresDM.Contains(p.IDCOLABORADOR)
                                                && estadosInbox.Contains(p.ESTADOSOLICITUD)
                                                && p.TipoSolicitud.CATEGORIA == CAT_MASTERDATA)
                                    .OrderBy(p => p.FECHACREACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

                if (solicitudes2 != null)
                {
                    solicitudes = solicitudes.Union(solicitudes2).Distinct(new ItemEqualityComparer()).ToList();
                }



            }
            else if (request.Tipo.Equals(INBOXADMIN))
            {
                // solo se obtienen las solicitudes que no son estados finales
                List<string> estadosInbox = new List<string>()
                {
                    "EP", "EA", "CO", "EPV"
                };

                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => p.IDUSUARIORESPONSABLE == request.IdUsuario
                                                && estadosInbox.Contains(p.ESTADOSOLICITUD)
                                                && p.TipoSolicitud.CATEGORIA == CAT_ADMINISTRATIVE)
                                    .OrderBy(p => p.FECHACREACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
            else if (request.Tipo.Equals(INBOXADMINDM))
            {
                // solo se obtienen las solicitudes que no son estados finales
                List<string> estadosInbox = new List<string>()
                {
                    "EP", "EA", "CO", "EPV"
                };

                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => p.IDUSUARIORESPONSABLE == request.IdUsuario
                                                && estadosInbox.Contains(p.ESTADOSOLICITUD)
                                                && p.TipoSolicitud.CATEGORIA == CAT_ADMINISTRATIVE)
                                    .OrderBy(p => p.FECHACREACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

                List<Colaborador> listColaboresDM = new List<Colaborador>();

                listColaboresDM = await _context.Colaboradores
                                .Where(p => p.IDDELIVERYMANAGER == request.IdUsuario).ToListAsync(cancellationToken);

                List<int> listIDColaboresDM = new List<int>();

                listIDColaboresDM = listColaboresDM.Select(p => p.IDCOLABORADOR).ToList();

                List<SolicitudDto> solicitudes2;

                solicitudes2 = await _context.SolicitudMasterData
                                    .Where(p => listIDColaboresDM.Contains((int)p.IDCOLABORADOR)
                                    //.Where(p => listIDColaboresDM.Contains(p.IDCOLABORADOR)
                                                && estadosInbox.Contains(p.ESTADOSOLICITUD)
                                                && p.TipoSolicitud.CATEGORIA == CAT_ADMINISTRATIVE)
                                    .OrderBy(p => p.FECHACREACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

                if (solicitudes2 != null)
                {
                    solicitudes = solicitudes.Union(solicitudes2).Distinct(new ItemEqualityComparer()).ToList();
                }

            }
            else if (request.Tipo.Equals(OUTBOX))
            {
                var listaSolicitudes = await _context.SolicitudesTramitadas(request.IdUsuario);
                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => listaSolicitudes.Contains(p.IDSOLICITUDMASTERDATA)
                                                 && p.TipoSolicitud.CATEGORIA == (string.IsNullOrEmpty(request.Categoria) ? "MD" : request.Categoria))
                                    .OrderBy(p => p.FECHACREACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
            else if (request.Tipo.Equals(OUTBOXADMIN))
            {
                var listaSolicitudes = await _context.SolicitudesAdministrativasTramitadas(request.IdUsuario);
                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => listaSolicitudes.Contains(p.IDSOLICITUDMASTERDATA)
                                                 && p.TipoSolicitud.CATEGORIA == CAT_ADMINISTRATIVE
                                                 && (p.IDUSUARIORESPONSABLE != request.IdUsuario
                                                     || (p.IDUSUARIORESPONSABLE == request.IdUsuario && p.ESTADOSOLICITUD != "CO")
                                                    )
                                                 )
                                    .OrderBy(p => p.FECHACREACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
            else if (request.Tipo.Equals(TRACKING))
            {
                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => estadosTracking.Contains(p.ESTADOSOLICITUD)
                                                 && p.TipoSolicitud.CATEGORIA == (string.IsNullOrEmpty(request.Categoria) ? "MD" : request.Categoria))
                                    .OrderBy(p => p.FECHAMODIFICACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
                solicitudes = GetRangoAntiguedad(solicitudes, estadosTracking);
            }
            else if (request.Tipo.Equals(TRACKINGADMIN))
            {
                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => estadosTrackingAdmin.Contains(p.ESTADOSOLICITUD)
                                                 && p.TipoSolicitud.CATEGORIA == (string.IsNullOrEmpty(request.Categoria) ? "AD" : request.Categoria)
                                                 && p.FECHACREACION >= request.FechaInicio && p.FECHACREACION <= request.FechaFin)
                                    .OrderBy(p => p.FECHAMODIFICACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
                solicitudes = GetRangoAntiguedad(solicitudes, estadosTrackingAdmin);
            }
            else if (request.Tipo.Equals(HISTORY))
            {
                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => p.IDCOLABORADOR == request.IdUsuario && p.ESTADOSOLICITUD == "AP"
                                                && p.TipoSolicitud.CATEGORIA == (string.IsNullOrEmpty(request.Categoria) ? "MD" : request.Categoria))
                                    .OrderByDescending(p => p.FECHAMODIFICACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
            else if (request.Tipo.Equals(CONSULTANTS))
            {
                solicitudes = await _context.SolicitudMasterData
                                    .Include(p => p.Colaborador)
                                    .Where(p => p.Colaborador.IDDELIVERYMANAGER == request.IdUsuario && p.ESTADOSOLICITUD != "RE"
                                                && p.TipoSolicitud.CATEGORIA == (string.IsNullOrEmpty(request.Categoria) ? "MD" : request.Categoria))
                                    .OrderBy(p => p.Colaborador.NOMBRE)
                                    .ThenBy(p => p.Colaborador.PRIMERAPELLIDO)
                                    .ThenBy(p => p.Colaborador.SEGUNDOAPELLIDO)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
            else //todas
            {
                solicitudes = await _context.SolicitudMasterData
                                    .Where(p => p.TipoSolicitud.CATEGORIA == (string.IsNullOrEmpty(request.Categoria) ? "MD" : request.Categoria))
                                    .OrderBy(p => p.FECHAMODIFICACION)
                                    .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
                solicitudes = GetRangoAntiguedad(solicitudes, estadosTracking);
            }

            var vm = new ListaSolicitudesVm
            {
                SolicitudesMasterData = solicitudes,
                count = solicitudes.Count
            };

            return vm;
        }

        private List<SolicitudDto> GetRangoAntiguedad(List<SolicitudDto> solicitudes, List<string> estadosTracK)
        {
            var parametros = _context.Parametross
               .ProjectTo<ParametrosDto>(_mapper.ConfigurationProvider)
               .ToList().FirstOrDefault();

            foreach (SolicitudDto solicitud in solicitudes)
            {
                solicitud.Rango = estadosTracK.Contains(solicitud.CodigoEstado)
                                  ? (solicitud.DiferenciaDias <= parametros.IndicadorSolicitudVerde
                                     ? RangoAntiguedad.Verde
                                     : (solicitud.DiferenciaDias <= parametros.IndicadorSolicitudAmarillo
                                        ? RangoAntiguedad.Amarillo
                                        : RangoAntiguedad.Rojo)
                                    )
                                  : RangoAntiguedad.Ninguno;
            }
            return solicitudes;
        }
    }

    public class ItemEqualityComparer : IEqualityComparer<SolicitudDto>
    {
        public bool Equals(SolicitudDto x, SolicitudDto y)
        {
            // Two items are equal if their keys are equal.
            return x.IdSolicitud == y.IdSolicitud;
        }

        public int GetHashCode(SolicitudDto obj)
        {
            return obj.IdSolicitud.GetHashCode();
        }
    }

}

