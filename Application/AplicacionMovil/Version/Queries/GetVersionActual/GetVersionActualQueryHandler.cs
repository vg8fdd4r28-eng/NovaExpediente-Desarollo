using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Version.Queries.GetVersionActual
{
    // Definir el objeto solicitud que se utilizará para obtener la versión actual.
    public class GetVersionActualQuery : IRequest<VersionVm> { }

    // Definir el controlador que procesará la solicitud y devolverá la información de la versión.
    public class GetVersionActualQueryHandler : IRequestHandler<GetVersionActualQuery, VersionVm>
    {
        private readonly INovaExpedienteDbContext _context;

        public GetVersionActualQueryHandler(INovaExpedienteDbContext context) => _context = context;

        public async Task<VersionVm> Handle(GetVersionActualQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Consultar la base de datos por el valor del catálogo de la versión activa.
                CatalogoValor catalogoValorVersion = await _context.CatalogoValores
                    .SingleOrDefaultAsync(x => x.ISACTIVO && x.CODCATALOGO == "PARAM" && x.CODCATALOGOVALOR == "VERAP", cancellationToken);

                if (catalogoValorVersion == null)
                {
                    throw new NotFoundException(nameof(CatalogoValor), "VERAP");
                }

                // Si el valor existe retornar un objeto VersionVm y definir sus propiedades según el valor del catálogo.
                return new VersionVm
                {
                    CodigoVersion = catalogoValorVersion.VALOR1,
                    NumCodVersion = int.TryParse(catalogoValorVersion.VALOR1?.Replace(".", ""), out var versionNumber) ? versionNumber : 0
                };

            }
            catch (Exception ex)
            {
                throw new QueryException("Error obteniendo la información de la version", ex);
            }
        }

    }

    // Definir excepción personalizada para errores de consulta.
    public class QueryException : Exception
    {
        public QueryException(string message)
            : base(message)
        {
        }

        public QueryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
