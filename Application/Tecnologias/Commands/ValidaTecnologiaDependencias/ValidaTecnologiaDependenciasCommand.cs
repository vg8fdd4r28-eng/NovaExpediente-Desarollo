using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Tecnologias.Commands.ValidaTecnologiaDependencias
{
    public class ValidaTecnologiaDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaTecnologiaDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaTecnologiaDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnExperiencia = await _context.ExperienciaTecnologias
               .Where(c => c.IDTECNOLOGIA == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, la tecnología cuenta con experiencias asociadas.";
                }


                var EnProyectos = await _context.ProyectoTecnologias
               .Where(c => c.IDTECNOLOGIA == request.Id)
               .ToListAsync(cancellationToken);

                if (EnProyectos.Count > 0)
                {
                    return res = "No se puede eliminar, la tecnología cuenta con proyectos asociados.";
                }




                return res;




            }
        }
    }
}
