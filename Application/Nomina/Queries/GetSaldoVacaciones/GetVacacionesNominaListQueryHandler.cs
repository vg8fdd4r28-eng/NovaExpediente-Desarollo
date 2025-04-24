using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Nomina.Queries.GetSaldoVacaciones
{
    public class GetVacacionesNominaListQueryHandler : IRequestHandler<GetVacacionesNominaListQuery, VacacionesNominaVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly INominaService _service;

        public GetVacacionesNominaListQueryHandler(INovaExpedienteDbContext context, INominaService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<VacacionesNominaVm> Handle(GetVacacionesNominaListQuery request, CancellationToken cancellationToken)
        {

            // var cv = await _context.ConsultaCV(request.Query);

            string res = await _service.ConsultaSaldo(request.IdNomina);

            var vm = new VacacionesNominaVm
            {
                Vacaciones = res
            };

            return vm;
        }
    }


}
