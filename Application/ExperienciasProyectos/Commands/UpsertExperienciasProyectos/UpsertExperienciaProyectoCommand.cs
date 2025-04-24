using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasProyectos.Commands.UpsertExperienciasProyectos
{
    public class UpsertExperienciaProyectoCommand : IRequest<int>
    {

        public int? Id { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBREPROYECTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBRECLIENTE { get; set; }
        public string INICIO { get; set; }
        public string FIN { get; set; }
        public string ROLPROYECTO { get; set; }
        public string LICITACION { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertExperienciaProyectoCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertExperienciaProyectoCommand request, CancellationToken cancellationToken)
            {
                ExperienciaProyecto entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.ExperienciaProyectos.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new ExperienciaProyecto();

                    _context.ExperienciaProyectos.Add(entity);
                }

                entity.IDCOLABORADOR = request.IDCOLABORADOR;
                entity.NOMBREPROYECTO = request.NOMBREPROYECTO;
                entity.DESCRIPCION = request.DESCRIPCION;
                entity.NOMBRECLIENTE = request.NOMBRECLIENTE;
                entity.INICIO = request.INICIO;
                entity.FIN = request.FIN;
                entity.ROLPROYECTO = request.ROLPROYECTO;
                entity.LICITACION = request.LICITACION;




                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDEXPERIENCIAPROYECTO;
            }
        }
    }
}
