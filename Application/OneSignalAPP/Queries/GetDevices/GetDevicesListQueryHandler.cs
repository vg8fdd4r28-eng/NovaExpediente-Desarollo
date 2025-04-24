using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.OneSignalAPP.Queries.GetDevices
{
    public class GetDevicesListQueryHandler : IRequestHandler<GetDevicesListQuery, string>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IOneSignalService _service;

        public GetDevicesListQueryHandler(INovaExpedienteDbContext context, IOneSignalService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<string> Handle(GetDevicesListQuery request, CancellationToken cancellationToken)
        {

            string res = await _service.ViewDevices();      

            return res;
        }
    }


}
