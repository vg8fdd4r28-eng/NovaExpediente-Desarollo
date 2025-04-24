using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.Referencias.Commands.UpsertReferencia
{
    public class UpsertReferenciaCommand : IRequest <int>
    {
        public int? Id { get; set; }

        public int IDCLIENTE { get; set; }
        public string IDENTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public string EXTENSION { get; set; }
        public string CELULAR { get; set; }
        public string PUESTO { get; set; }
        public bool ACTIVO { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertReferenciaCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertReferenciaCommand request, CancellationToken cancellationToken)
            {
                Referencia entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Referenciass.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Referencia();

                    _context.Referenciass.Add(entity);
                }

                entity.IDCLIENTE = request.IDCLIENTE;
                entity.IDENTIFICACION = request.IDENTIFICACION;
                entity.NOMBRE = request.NOMBRE;
                entity.CORREO = request.CORREO;
                entity.EXTENSION = request.EXTENSION;
                entity.CELULAR = request.CELULAR;
                entity.PUESTO = request.PUESTO;
                entity.ACTIVO = request.ACTIVO;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDREFERENCIA;
            }
        }
    }
}
