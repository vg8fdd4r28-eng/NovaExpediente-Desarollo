using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.EmpresasCorporacion.Commands.ValidaEmpresaDependencias
{
    public class ValidaEmpresaDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaEmpresaDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaEmpresaDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var EnColaborador = await _context.Colaboradores
               .Where(c => c.IDEMPRESACORPORACION == request.Id)
               .ToListAsync(cancellationToken);

                if (EnColaborador.Count > 0)
                {
                    return res = "No se puede eliminar, la empresa cuenta con colaboradores asociados.";
                }

                var Encliente = await _context.Clientess
                .Where(c => c.IDEMPRESACORPORACION == request.Id)
                .ToListAsync(cancellationToken);

                if (Encliente.Count > 0)
                {
                    return res = "No se puede eliminar, la empresa cuenta con clientes asociados.";
                }

                var EnExperiencia = await _context.ExperienciaProyectos
               .Where(c => c.IDEMPRESACORPORACION == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, la empresa cuenta con experiencias asociadas.";
                }

                return res;




            }
        }
    }
}
