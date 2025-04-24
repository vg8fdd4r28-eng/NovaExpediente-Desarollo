using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetFotoColaborador
{
    public class GetPhotoByIdentityCardHandler : IRequestHandler<GetPhotoByIdentityCardQuery, FotoColaboradorVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;




        public GetPhotoByIdentityCardHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FotoColaboradorVm> Handle(GetPhotoByIdentityCardQuery request, CancellationToken cancellationToken)
        {
            var colaborador = await _context.Colaboradores
                .Where(c => c.IDENTIFICACION == request.Identificacion)
                .ProjectTo<FotoColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new FotoColaboradorVm
            {
                FotoColaborador = colaborador.FirstOrDefault()
            };

            return vm;
        }


    }
}
