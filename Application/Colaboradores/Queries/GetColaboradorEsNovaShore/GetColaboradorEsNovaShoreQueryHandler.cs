using AutoMapper;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Security.Principal;
using System.Security; 
using Microsoft.EntityFrameworkCore;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorEsNovaShore
{
    public class GetColaboradorEsNovaShoreQueryHandler : IRequestHandler<GetColaboradorEsNovaShoreQuery, bool>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetColaboradorEsNovaShoreQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(GetColaboradorEsNovaShoreQuery request, CancellationToken cancellationToken)
        {
            bool result = false;

            int novashore = _context.EmpresaCorporacions
            .Where(s => s.NOMBRE.Trim().ToUpper() == "NOVASHORE")
            .Select(s => s.IDEMPRESACORPORACION)
            .FirstOrDefault();

            if (_context.Colaboradores.Any(x => x.CORREO == request.Email && x.IDEMPRESACORPORACION == novashore))
            {
                result = true;
            }

            return result;

        }
    }
}
