using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Empresas.Commands.UpsertEmpresas
{
    public class UpsertEmpresasCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public string cedulaJuridica { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertEmpresasCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertEmpresasCommand request, CancellationToken cancellationToken)
            {
                Empresa entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Empresas.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Empresa();

                    _context.Empresas.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
                entity.CEDULAJURIDICA = request.cedulaJuridica;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDEMPRESA;
            }
        }
    }
}
