using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Parametros.Queries.GetParametros
{
    public class GetParametrosQueryHandler : IRequestHandler<GetParametrosQuery, ParametrosVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetParametrosQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParametrosVm> Handle(GetParametrosQuery request, CancellationToken cancellationToken)
        {
            var parametros = await _context.Parametross
                .ProjectTo<ParametrosDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ParametrosVm()
            {
                Parametros = parametros.FirstOrDefault()
            };

            return vm;
        }


    }
}
