using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Clientes.Commands.ValidaClienteDependencias
{
    public class ValidaClienteDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaClienteDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
          

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaClienteDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "OK";

                var EnColaborador = await _context.Colaboradores
               .Where(c => c.IDCLIENTE == request.Id && c.ACTIVO)
               .ToListAsync(cancellationToken);

                if (EnColaborador.Count > 0)
                {
                    return res = "No se puede eliminar, el cliente cuenta con colaboradores asociados.";
                }

                var EnExpProyectos = await _context.ExperienciaProyectos
                .Where(c => c.IDCLIENTE == request.Id)
                .ToListAsync(cancellationToken);

                if (EnExpProyectos.Count > 0)
                {
                    return res = "No se puede eliminar, el cliente cuenta con experiencia en proyectos asociada.";

                }


                var EnProyectos = await _context.Proyectoss
                .Where(c => c.IDCLIENTE == request.Id && c.ACTIVO)
                .ToListAsync(cancellationToken);

                if (EnProyectos.Count > 0)
                {
                    return res = "No se puede eliminar, el cliente cuenta con proyectos asociados.";
                }


                return res;




            }
        }
    }
}
