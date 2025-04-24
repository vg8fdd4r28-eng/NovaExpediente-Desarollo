
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Entidad.Commands.UpsertEntidad
{
    public class UpsertVacunasColaboradorCommand : IRequest<long>
    {
        public int? IdVacunasColaborador { get; set; }
        public int IdUsuarioCrea { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public int IdVacunacionColaborador { get; set; }
        public int IdVacuna { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public class UpsertVacunasColaboradorCommandHandler : IRequestHandler<UpsertVacunasColaboradorCommand, long>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertVacunasColaboradorCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(UpsertVacunasColaboradorCommand request, CancellationToken cancellationToken)
            {
                VacunasColaborador entity;

                if (request.IdVacunasColaborador.HasValue && request.IdVacunasColaborador.Value != 0)
                {
                    entity = await _context.VacunasColaboradores.FindAsync(request.IdVacunasColaborador.Value);
                }
                else
                {
                    entity = new VacunasColaborador();

                    _context.VacunasColaboradores.Add(entity);
                }

                entity.IdVacunasColaborador = request.IdVacunasColaborador.Value;
                entity.IdVacunacionColaborador = request.IdVacunacionColaborador;
                entity.IdVacuna = request.IdVacuna;
                entity.Fecha = request.Fecha;
                
                await _context.SaveChangesAsync(cancellationToken);

                return entity.IdVacunasColaborador;
            }
        }
    }
}
