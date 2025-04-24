
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Entidad.Commands.UpsertEntidad
{
    public class UpsertVacunacionColaboradorCommand : IRequest<long>
    {
        public int? IdVacunacionColaborador { get; set; }
        public int IdUsuarioCrea { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public int IdColaborador { get; set; }
        public byte[] Certificado { get; set; }
        public string MimeType { get; set; }
        public char Estado { get; set; }
        public char? TipoDocumento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public class UpsertVacunacionColaboradorCommandHandler : IRequestHandler<UpsertVacunacionColaboradorCommand, long>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertVacunacionColaboradorCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(UpsertVacunacionColaboradorCommand request, CancellationToken cancellationToken)
            {
                VacunacionColaborador entity;

                if (request.IdVacunacionColaborador.HasValue && request.IdVacunacionColaborador.Value != 0)
                {
                    entity = await _context.VacunacionColaboradores.FindAsync(request.IdVacunacionColaborador.Value);
                }
                else
                {
                    entity = new VacunacionColaborador();

                    _context.VacunacionColaboradores.Add(entity);
                }

                entity.IdVacunacionColaborador = request.IdVacunacionColaborador.Value;
                entity.IdColaborador = request.IdColaborador;
                entity.Certificado = request.Certificado;
                entity.MimeType = request.MimeType;
                entity.Estado = request.Estado;
                entity.TipoDocumento = request.TipoDocumento;
                
                await _context.SaveChangesAsync(cancellationToken);

                return entity.IdVacunacionColaborador;
            }
        }
    }
}
