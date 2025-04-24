using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AreasOrganizativas.Commands.ValidaAreaOrganizativaDependencias
{
    public class ValidaAreaOrganizativaDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaAreaOrganizativaDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaAreaOrganizativaDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var EnColaboradores = await _context.Colaboradores
               .Where(c => c.IDAREAORGANIZATIVA == request.Id)
               .ToListAsync(cancellationToken);

                if (EnColaboradores.Count > 0)
                {
                    return res = "No se puede eliminar, el área organizativa cuenta con colaboradores asociados.";
                }



                return res;




            }
        }
    }
}
