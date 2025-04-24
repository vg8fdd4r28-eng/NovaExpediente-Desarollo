using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Idiomas.Commands.ValidaIdiomaDependencias
{
    public class ValidaIdiomaDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaIdiomaDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaIdiomaDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnColaborador = await _context.IdiomaColaboradors
               .Where(c => c.IDIDIOMA == request.Id)
               .ToListAsync(cancellationToken);

                if (EnColaborador.Count > 0)
                {
                    return res = "No se puede eliminar, el idioma cuenta con colaboradores asociados.";
                }

                var EnNivel = await _context.NivelIdiomas
          .Where(c => c.IDIDIOMA == request.Id)
          .ToListAsync(cancellationToken);

                if (EnColaborador.Count > 0)
                {
                    return res = "No se puede eliminar, el idioma cuenta con niveles asociados.";
                }

                return res;




            }
        }
    }
}
