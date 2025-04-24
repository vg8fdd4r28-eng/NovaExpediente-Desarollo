using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Domain.Entities;
using NovaExpediente.Application.Common.Interfaces;



namespace NovaExpediente.Application.MasterData.Commands.CambiarResponsable
{
    public class CambiarResponsableCommand : IRequest<int>
    {

        public int? Id { get; set; }

        public int IdResponsable { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<CambiarResponsableCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CambiarResponsableCommand request, CancellationToken cancellationToken)
            {
                return await _context.CambiarResponsable(request.Id.Value, request.IdResponsable);
                
            }
        }

    }
}
