using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.BasesDatos.Commands.ValidaBaseDatosDependencias
{
    public class ValidaBaseDatosDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaBaseDatosDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaBaseDatosDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var EnExperiencia = await _context.ExperienciasBaseDatos
               .Where(c => c.IDBASEDATOS == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, la base de datos cuenta con experiencias asociadas.";
                }

                var EnProyecto = await _context.ProyectoBaseDatoss
          .Where(c => c.IDBASEDATOS == request.Id)
          .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, la base de datos cuenta con proyectos asociados.";
                }


                return res;




            }
        }
    }
}
