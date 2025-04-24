using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Domain.Entities;
using NovaExpediente.Application.Common.Interfaces;



namespace NovaExpediente.Application.Colaboradores.Commands.AsociarEmpresaColaborador
{
    public class AsociarEmpresaColaboradorCommand : IRequest<int>
    {

        public int? Id { get; set; }

        public int IdEmpresaCorporacion { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<AsociarEmpresaColaboradorCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(AsociarEmpresaColaboradorCommand request, CancellationToken cancellationToken)
            {
               Colaborador entity;

                if (request.Id.HasValue)
                {
                    entity = await _context.Colaboradores.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Colaborador();

                    _context.Colaboradores.Add(entity);
                }

                entity.IDEMPRESACORPORACION = request.IdEmpresaCorporacion;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDCOLABORADOR;
            }
        }

    }
}
