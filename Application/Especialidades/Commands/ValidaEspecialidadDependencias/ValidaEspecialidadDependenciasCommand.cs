using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Especialidades.Commands.ValidaEspecialidadDependencias
{
    public class ValidaEspecialidadDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaEspecialidadDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaEspecialidadDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnColaborador = await _context.EspecialidadConsultors
               .Where(c => c.IDESPECIALIDAD == request.Id)
               .ToListAsync(cancellationToken);

                if (EnColaborador.Count > 0)
                {
                    return res = "No se puede eliminar, la especialidad cuenta con colaboradores asociados.";
                }



                return res;




            }
        }
    }
}
