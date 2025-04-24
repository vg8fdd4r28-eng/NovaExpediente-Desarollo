using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetExperienciaActual
{
    public class GetExperienciaActualQueryHandler : IRequestHandler<GetExperienciaActualQuery, ExperienciaActualVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaActualQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaActualVm> Handle(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            ExperienciaActualVm vm = new ExperienciaActualVm();

            switch (request.IdCatalogo)
            {
                case 1: //'Base de datos',
                    vm = await ExperienciaBaseDatos(request, cancellationToken);
                    break;
                case 2: //'Lenguajes',
                    vm = await ExperienciaLenguaje(request, cancellationToken);
                    break;
                case 3: //'Ambientes de desarrollo',
                    vm = await ExperienciaAmbienteDesarrollo(request, cancellationToken);
                    break;
                case 4: //'Tecnologías',
                    vm = await ExperienciaTecnologia(request, cancellationToken);
                    break;
                case 5: //'ERPs / Core',
                    vm = await ExperienciaErp(request, cancellationToken);
                    break;
                case 6: //'Calidad',
                    vm = await ExperienciaCalidad(request, cancellationToken);
                    break;
                case 7: //'Infraestructura',
                    vm = await ExperienciaInfraestructura(request, cancellationToken);
                    break;
                case 8: //'Buses y SOA',
                    vm = await ExperienciaBus(request, cancellationToken);
                    break;
                case 9: //'BPM'
                    vm = await ExperienciaBpm(request, cancellationToken);
                    break;
                case 10: //'Cloud'
                    vm = await ExperienciaCloud(request, cancellationToken);
                    break;
            }            

            return vm;
        }

        private async Task<ExperienciaActualVm> ExperienciaBpm(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaBPMs
                                       .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDBPM == request.IdInstanciaCatalogo)
                                       .Select(e => new
                                       {
                                           FechaCreacion = e.FECHACREACION,
                                           FechaModificacion = e.FECHAMODIFICACION,
                                           Meses = e.MESESEXPERIENCIA
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm() { Meses = null, UltimaActualizacion = null};
        }

        private async Task<ExperienciaActualVm> ExperienciaBus(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaBuss
                                       .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDBUS == request.IdInstanciaCatalogo)
                                       .Select(e => new
                                       {
                                           FechaCreacion = e.FECHACREACION,
                                           FechaModificacion = e.FECHAMODIFICACION,
                                           Meses = e.MESESEXPERIENCIA
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm();
        }

        private async Task<ExperienciaActualVm> ExperienciaInfraestructura(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaInfraestructuras
                                       .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDINFRAESTRUCTURA == request.IdInstanciaCatalogo)
                                       .Select(e => new
                                       {
                                           FechaCreacion = e.FECHACREACION,
                                           FechaModificacion = e.FECHAMODIFICACION,
                                           Meses = e.MESESEXPERIENCIA
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm();
        }

        private async Task<ExperienciaActualVm> ExperienciaCalidad(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaCalidads
                                      .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDCALIDAD == request.IdInstanciaCatalogo)
                                      .Select(e => new
                                      {
                                          FechaCreacion = e.FECHACREACION,
                                          FechaModificacion = e.FECHAMODIFICACION,
                                          Meses = e.MESESEXPERIENCIA
                                      })
                                      .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm();
        }

        private async Task<ExperienciaActualVm> ExperienciaErp(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaERPs
                                      .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDERPCORE == request.IdInstanciaCatalogo)
                                      .Select(e => new
                                      {
                                          FechaCreacion = e.FECHACREACION,
                                          FechaModificacion = e.FECHAMODIFICACION,
                                          Meses = e.MESESEXPERIENCIA
                                      })
                                      .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm();
        }

        private async Task<ExperienciaActualVm> ExperienciaTecnologia(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaTecnologias
                                      .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDTECNOLOGIA == request.IdInstanciaCatalogo)
                                      .Select(e => new
                                      {
                                          FechaCreacion = e.FECHACREACION,
                                          FechaModificacion = e.FECHAMODIFICACION,
                                          Meses = e.MESESEXPERIENCIA
                                      })
                                      .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm();
        }

        private async Task<ExperienciaActualVm> ExperienciaAmbienteDesarrollo(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaAmbienteDesarrollos
                                      .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDAMBIENTEDESARROLLO == request.IdInstanciaCatalogo)
                                      .Select(e => new
                                      {
                                          FechaCreacion = e.FECHACREACION,
                                          FechaModificacion = e.FECHAMODIFICACION,
                                          Meses = e.MESESEXPERIENCIA
                                      })
                                      .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm();
        }

        private async Task<ExperienciaActualVm> ExperienciaLenguaje(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaLenguajes
                                       .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDLENGUAJEPROGRAMACION == request.IdInstanciaCatalogo)
                                       .Select(e => new
                                       {
                                           FechaCreacion = e.FECHACREACION,
                                           FechaModificacion = e.FECHAMODIFICACION,
                                           Meses = e.MESESEXPERIENCIA
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm();
        }

        private async Task<ExperienciaActualVm> ExperienciaBaseDatos(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciasBaseDatos
                                       .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDBASEDATOS == request.IdInstanciaCatalogo)
                                       .Select(e => new
                                       {
                                           FechaCreacion = e.FECHACREACION,
                                           FechaModificacion = e.FECHAMODIFICACION,
                                           Meses = e.MESESEXPERIENCIA
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm() { Meses = null, UltimaActualizacion = null };
        }

        private async Task<ExperienciaActualVm> ExperienciaCloud(GetExperienciaActualQuery request, CancellationToken cancellationToken)
        {
            var experiencia = await _context.ExperienciaClouds
                                      .Where(e => e.IDCOLABORADOR == request.IdColaborador && e.IDCLOUD == request.IdInstanciaCatalogo)
                                      .Select(e => new
                                      {
                                          FechaCreacion = e.FECHACREACION,
                                          FechaModificacion = e.FECHAMODIFICACION,
                                          Meses = e.MESESEXPERIENCIA
                                      })
                                      .FirstOrDefaultAsync(cancellationToken);
            if (experiencia != null)
            {
                return new ExperienciaActualVm()
                {
                    UltimaActualizacion = experiencia.FechaModificacion.HasValue ? experiencia.FechaModificacion : experiencia.FechaCreacion,
                    Meses = experiencia.Meses
                };
            }
            else
                return new ExperienciaActualVm();
        }

    }
}
