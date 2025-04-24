using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace NovaExpediente.Application.Colaboradores.Queries.GetCantidadColaboradoresDM
{
    public class GetCantidadColaboradoresDMListQueryHandler : IRequestHandler<GetCantidadColaboradoresDMListQuery, CantidadColaboradoresDMListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;



        public GetCantidadColaboradoresDMListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = new MachineDateTime();
        }

        public async Task<CantidadColaboradoresDMListVm> Handle(GetCantidadColaboradoresDMListQuery request, CancellationToken cancellationToken)
        {

            // cantidad de colaoradores por DM
            var colaboradores = await _context.Colaboradores
                .Where(d => d.IDDELIVERYMANAGER != null && d.ACTIVO && d.DeliveryManager.ACTIVO)
                .GroupBy(d => new
                {
                    d.DeliveryManager.NOMBRE,
                    d.DeliveryManager.PRIMERAPELLIDO,
                    d.DeliveryManager.SEGUNDOAPELLIDO,
                    d.DeliveryManager.FECHAINGRESO,
                    d.IDDELIVERYMANAGER,
                })
                .Select(b => new CantidadColaboradoresDMDto
                {
                    IdDeliveryManager = b.Key.IDDELIVERYMANAGER,
                    IdColaborador = b.Key.IDDELIVERYMANAGER,
                    Nombre = b.Key.NOMBRE + " " + b.Key.PRIMERAPELLIDO + " " + b.Key.SEGUNDOAPELLIDO,
                    Cantidad = b.Count(),
                    FechaIngreso = _dateTime.Seniority(b.Key.FECHAINGRESO, DateTime.Now)                })
                .ToListAsync(cancellationToken);

            // estado de la actualización de expedientes por DM
            var estadoExpediente = await _context.ActualizacionExpedientePorDM();

            var estadoCurriculum = await _context.ActualizacionCurriculumPorDM();

            foreach (var item in colaboradores)
            {
                item.ActualizacionExpediente = estadoExpediente[item.IdColaborador.Value].Values.ToList();
                item.ActualizacionCurriculum = estadoCurriculum[item.IdColaborador.Value].Values.ToList();
            }
            var vm = new CantidadColaboradoresDMListVm
            {
                Colaboradores = colaboradores.OrderBy(b => b.Nombre).ToList(),
                Count = colaboradores.Count()
            };

            return vm;
        }        
    }
}
