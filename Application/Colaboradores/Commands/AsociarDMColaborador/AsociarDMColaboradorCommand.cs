using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Domain.Entities;
using NovaExpediente.Application.Common.Interfaces;



namespace NovaExpediente.Application.Colaboradores.Commands.AsociarDMColaborador
{
    public class AsociarDMColaboradorCommand : IRequest<int>
    {

        public int? Id { get; set; }

        public int IdDM { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<AsociarDMColaboradorCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(AsociarDMColaboradorCommand request, CancellationToken cancellationToken)
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

                entity.IDDELIVERYMANAGER = request.IdDM;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDCOLABORADOR;
            }
        }

    }
}
