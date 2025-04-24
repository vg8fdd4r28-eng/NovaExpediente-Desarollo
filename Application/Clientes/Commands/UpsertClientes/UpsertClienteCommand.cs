using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Clientes.Commands.UpsertClientes
{
    public class UpsertClienteCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public int IDSECTOR { get; set; }
        public int IDPAIS { get; set; }
        public string CODIGOCLIENTE { get; set; }
        public string CEDULAJURIDICA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public bool ACTIVO { get; set; }
        public string TIPOCLIENTE { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertClienteCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertClienteCommand request, CancellationToken cancellationToken)
            {
                Cliente entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Clientess.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Cliente();

                    _context.Clientess.Add(entity);
                }

                entity.IDSECTOR = request.IDSECTOR;
                entity.IDPAIS = request.IDPAIS;
                entity.CODIGOCLIENTE = request.CODIGOCLIENTE;
                entity.CEDULAJURIDICA = request.CEDULAJURIDICA;
                entity.NOMBRE = request.NOMBRE;
                entity.DESCRIPCION = request.DESCRIPCION;
                entity.CORREO = request.CORREO;
                entity.TELEFONO = request.TELEFONO;
                entity.ACTIVO = request.ACTIVO;
                entity.TIPOCLIENTE = request.TIPOCLIENTE;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDCLIENTE;
            }
        }
    }
}
