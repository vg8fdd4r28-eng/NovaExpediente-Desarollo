using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetDetalleSolicitud
{
    public class GetDetalleSolicitudQueryHandler : IRequestHandler<GetDetalleSolicitudQuery, DetalleSolicitudVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetDetalleSolicitudQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DetalleSolicitudVm> Handle(GetDetalleSolicitudQuery request, CancellationToken cancellationToken)
        {
            //var solicitud = await _context.SolicitudMasterData
            //    .Include(p => p.Comentarios)
            //    .Where(p => p.IDSOLICITUDMASTERDATA == request.Id)
            //    .ProjectTo<DetalleSolicitudDto>(_mapper.ConfigurationProvider)
            //    .ToListAsync(cancellationToken);

            //var vm = new DetalleSolicitudVm
            //{
            //    SolicitudMasterData = solicitud.FirstOrDefault()
            //};

            var vm = new DetalleSolicitudVm
            {
                SolicitudMasterData = await  _context.ObtenerDetalleSolicitud(request.Id)
            }; 

            vm.SolicitudMasterData.Comentarios = vm.SolicitudMasterData.Comentarios.OrderByDescending(p => p.Fecha).ToList();
            return vm;
        }
    }
}
