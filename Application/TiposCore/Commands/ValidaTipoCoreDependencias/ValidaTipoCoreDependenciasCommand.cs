using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.TiposCore.Commands.ValidaTipoCoreDependencias
{
    public class ValidaTipoCoreDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaTipoCoreDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaTipoCoreDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnERP = await _context.ErpCores
               .Where(c => c.IDTIPOCORE == request.Id)
               .ToListAsync(cancellationToken);

                if (EnERP.Count > 0)
                {
                    return res = "No se puede eliminar, el tipo de core cuenta con ERP's asociados.";
                }



                return res;




            }
        }
    }
}
