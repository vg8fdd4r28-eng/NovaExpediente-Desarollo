using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacaciones
{
    public class GetColaboradorVacacionesListKeyQueryHandler : IRequestHandler<GetColaboradorVacacionesListKeyQuery, ColaboradorVacacionesVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;




        public GetColaboradorVacacionesListKeyQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradorVacacionesVm> Handle(GetColaboradorVacacionesListKeyQuery request, CancellationToken cancellationToken)
        {
            var colaborador = await _context.Colaboradores
                .Where(c => c.IDCOLABORADOR == request.Id)
                .ProjectTo<ColaboradorVacacionesDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            CatalogoValor catalogoValorVacaciones = await _context.CatalogoValores
                .SingleOrDefaultAsync(x => x.ISACTIVO && x.CODCATALOGO == "PARAM" && x.CODCATALOGOVALOR == "MSV", cancellationToken);

            var vm = new ColaboradorVacacionesVm
            {
                ColaboradorVacaciones = colaborador,
                MuestraSaldoVacaciones =  this.MuestraSaldos(colaborador, catalogoValorVacaciones)
            };



            return vm;
        }

        public bool MuestraSaldos(ColaboradorVacacionesDto colaborador, CatalogoValor catalogo)
        {

            if (colaborador.IDNOMINA > 0 &  catalogo.VALOR2 == "FALSE")
            {
                return false;
            }

            if (colaborador.ESSERVICIOPROFESIONAL & catalogo.VALOR4 == "FALSE")
            {
                return false;
            }

            return true;


        }


    }
}
