
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Entidad.Commands.UpsertEntidad
{
    public class UpsertVacunaCommand : IRequest<long>
    {
                public int? IdVacuna {get; set;}
                public int IdUsuarioCrea {get; set;}
                public int? IdUsuarioModifica {get; set;}
                public string Nombre {get; set;}
                public DateTime? FechaCreacion {get; set;}
                public DateTime? FechaModificacion {get; set;}
        
        public class UpsertVacunaCommandHandler : IRequestHandler<UpsertVacunaCommand, long>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertVacunaCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(UpsertVacunaCommand request, CancellationToken cancellationToken)
            {
                Vacuna entity;

                if (request.IdVacuna.HasValue && request.IdVacuna.Value != 0)
                {
                    entity = await _context.Vacunas.FindAsync(request.IdVacuna.Value);
                }
                else
                {
                    entity = new Vacuna();

                    _context.Vacunas.Add(entity);
                }

                                entity.IdVacuna = request.IdVacuna.Value;
                                entity.Nombre = request.Nombre;
                               
                
                await _context.SaveChangesAsync(cancellationToken);

                return entity.IdVacuna;
            }
        }
    }
}
