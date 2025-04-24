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

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaHerramientas
{
    public class GetListaHerramientasQueryHandler : IRequestHandler<GetListaHerramientasQuery, ListaHerramientasVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetListaHerramientasQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaHerramientasVm> Handle(GetListaHerramientasQuery request, CancellationToken cancellationToken)
        {
            var ambientesDesarrollo = await _context.AmbienteDesarrollos
                .OrderBy(e => e.NOMBRE)
                .Select(e => new HerramientaParaProyectoDto { key = e.NOMBRE, value = e.NOMBRE})
                .ToListAsync(cancellationToken);

            var lenguajes = await _context.LenguajeProgramacions
                .OrderBy(e => e.NOMBRE)
                .Select(e => new HerramientaParaProyectoDto { key = e.NOMBRE, value = e.NOMBRE })
                .ToListAsync(cancellationToken);

            var basesDatos = await _context.BasesDatos
                .OrderBy(e => e.NOMBRE)
                .Select(e => new HerramientaParaProyectoDto { key = e.NOMBRE, value = e.NOMBRE })
                .ToListAsync(cancellationToken);

            var calidad = await _context.Calidads
                .OrderBy(e => e.NOMBRE)
                .Select(e => new HerramientaParaProyectoDto { key = e.NOMBRE, value = e.NOMBRE })
                .ToListAsync(cancellationToken);

            var infraestructura = await _context.Infraestructuras
                .OrderBy(e => e.NOMBRE)
                .Select(e => new HerramientaParaProyectoDto { key = e.NOMBRE, value = e.NOMBRE })
                .ToListAsync(cancellationToken);

            var busesSOA = await _context.Buss
                .OrderBy(e => e.Nombre)
                .Select(e => new HerramientaParaProyectoDto { key = e.Nombre, value = e.Nombre })
                .ToListAsync(cancellationToken);

            var tecnologias = await _context.Tecnologias
               .OrderBy(e => e.NOMBRE)
               .Select(e => new HerramientaParaProyectoDto { key = e.NOMBRE, value = e.NOMBRE })
               .ToListAsync(cancellationToken);

            var herramientas = ambientesDesarrollo
                        .Union(lenguajes)
                        .Union(basesDatos)
                        .Union(calidad)
                        .Union(infraestructura)
                        .Union(busesSOA)
                        .OrderBy(e => e.key)
                        .Select(e => new HerramientaParaProyectoDto { key = e.key, value = e.value })
                        .ToList();

            var vm = new ListaHerramientasVm
            {
                Herramientas = herramientas,
                Tecnologias = tecnologias,
                Count = herramientas.Count
            };

            return vm;
        }


    }

}

