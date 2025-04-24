using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetProyectoClienteValido
{
    public class GetProyectoClienteValidoQueryHandler : IRequestHandler<GetProyectoClienteValidoQuery, ProyectoClienteValidoVm>
    {
        private readonly INovaExpedienteDbContext _context;

        public GetProyectoClienteValidoQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }

        public async Task<ProyectoClienteValidoVm> Handle(GetProyectoClienteValidoQuery request, CancellationToken cancellationToken)
        {
            bool esValido = true;
            // se obtienen las experiencias del colaborador según nombre del cliente y nombre del proyecto
            var proyectos = await _context.ExperienciaProyectos
                .Where(e => e.IDCOLABORADOR == request.IdColaborador &&
                            !string.IsNullOrEmpty(request.NombreCliente) &&
                            (e.NOMBRECLIENTE.Trim() == request.NombreCliente.Trim() ||
                             e.Cliente.NOMBRE.Trim() == request.NombreCliente.Trim()) &&
                            e.NOMBREPROYECTO.Trim() == request.NombreProyecto.Trim())
                .Select(e => new ProyectoClienteValidoDto
                {
                    IdExperienciaProyecto = e.IDEXPERIENCIAPROYECTO,
                    FechaInicio = e.FECHAINICIO.Value,
                    FechaFin = e.FECHAFINAL
                })
                .ToListAsync(cancellationToken);

            foreach (ProyectoClienteValidoDto item in proyectos)
            {
                if (AreSpansOverlapping(new Tuple<DateTime, DateTime?>(request.FechaInicio, request.FechaFin),
                                        new Tuple<DateTime, DateTime?>(item.FechaInicio, item.FechaFin),
                                        true    //or use False, to ignore span's endpoints
                                        ))
                {
                    esValido = false;
                    break;
                }

                //Union(proyectosNova).ToList();
                //proyectos = proyectos.Distinct().ToList();

            }

            var vm = new ProyectoClienteValidoVm
            {
                NuevaExperienciaValida = esValido
            };

            return vm;
        }

        private bool AreSpansOverlapping(Tuple<DateTime, DateTime?> span1, Tuple<DateTime, DateTime?> span2, bool includeEndPoints)
        {
            if (span1 == null || span2 == null)
            {
                return false;
            }
            else if ((new DateTime[] { span1.Item1,
                                       span1.Item2.HasValue ? span1.Item2.Value : DateTime.MaxValue,
                                       span2.Item1, 
                                       span2.Item2.HasValue ? span2.Item2.Value : DateTime.MaxValue }).Any(v => v == DateTime.MinValue))
            {
                return false;
            }
            else
            {
                if (span1.Item1 > span1.Item2)
                {

                    span1 = new Tuple<DateTime, DateTime?>(span1.Item2.HasValue ? span1.Item2.Value : DateTime.MaxValue, span1.Item1);
                }
                if (span2.Item1 > span2.Item2)
                {
                    span2 = new Tuple<DateTime, DateTime?>(span2.Item2.HasValue ? span2.Item2.Value : DateTime.MaxValue, span2.Item1);
                }

                if (includeEndPoints)
                {
                    return
                    ((
                        (span1.Item1 <= span2.Item1 && span1.Item2 >= span2.Item1)
                        || (span1.Item1 <= span2.Item2 && span1.Item2 >= span2.Item2)
                    ) || (
                        (span2.Item1 <= span1.Item1 && span2.Item2 >= span1.Item1)
                        || (span2.Item1 <= span1.Item2 && span2.Item2 >= span1.Item2)
                    ));
                }
                else
                {
                    return
                    ((
                        (span1.Item1 < span2.Item1 && span1.Item2 > span2.Item1)
                        || (span1.Item1 < span2.Item2 && span1.Item2 > span2.Item2)
                    ) || (
                        (span2.Item1 < span1.Item1 && span2.Item2 > span1.Item1)
                        || (span2.Item1 < span1.Item2 && span2.Item2 > span1.Item2)
                    ) || (
                        span1.Item1 == span2.Item1 && span1.Item2 == span2.Item2
                    ));
                }
            }
        }

    }
}
