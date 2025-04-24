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

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetProyectosCliente
{
    public class GetProyectosClienteQueryHandler : IRequestHandler<GetProyectosClienteQuery, ListaProyectosClienteVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProyectosClienteQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaProyectosClienteVm> Handle(GetProyectosClienteQuery request, CancellationToken cancellationToken)
        {
            var proyectosSinNova = await _context.ExperienciaProyectos
                .Where(e =>  e.NOMBREPROYECTO.Trim() != string.Empty &&
                             !string.IsNullOrEmpty(request.NombreCliente) && e.NOMBRECLIENTE.Trim() == request.NombreCliente.Trim())
                .OrderBy(e => e.NOMBREPROYECTO)
                .Select(e => new ProyectoClienteDto { NombreProyecto = e.NOMBREPROYECTO.Trim() })
                .ToListAsync(cancellationToken);

            var proyectosNova = await _context.ExperienciaProyectos
                .Where(e => (!string.IsNullOrEmpty(request.NombreCliente)  && e.Cliente.NOMBRE.Trim() == request.NombreCliente.Trim())
                            || (request.IdCliente.HasValue && e.IDCLIENTE == request.IdCliente.Value))
                .OrderBy(e => e.NOMBREPROYECTO)
                .Select(e => new ProyectoClienteDto { NombreProyecto = e.NOMBREPROYECTO })
                .ToListAsync(cancellationToken);

            var proyectos = proyectosSinNova.Union(proyectosNova).GroupBy(x => x.NombreProyecto).Select(g => g.First()).ToList();

            //Union(proyectosNova).ToList();
            //proyectos = proyectos.Distinct().ToList();

            var vm = new ListaProyectosClienteVm
            {
                Proyectos = proyectos,
                Count = proyectos.Count
            };

            return vm;
        }


    }
}
