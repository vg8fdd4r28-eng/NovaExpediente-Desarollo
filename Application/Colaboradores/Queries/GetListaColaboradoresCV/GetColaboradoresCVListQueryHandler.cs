using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresCV
{
    class GetColaboradoresCVListQueryHandler : IRequestHandler<GetColaboradoresCVListQuery, ColaboradoresCVVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly ICurriculumService _service;

        public GetColaboradoresCVListQueryHandler(INovaExpedienteDbContext context, ICurriculumService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<ColaboradoresCVVm> Handle(GetColaboradoresCVListQuery request, CancellationToken cancellationToken)
        {

            var cv = await _context.ConsultaCV(request.Query);

            ResultadoGeneracionVM resultado;
            Dictionary<string, object> resultadoGeneracion;
            resultadoGeneracion = _service.GenerarCV(cv.JsonResult, request.TipoCV);

            resultado = new ResultadoGeneracionVM()
            {
                resultado = (int)resultadoGeneracion["Resultado"],
                mensaje = resultadoGeneracion["Mensaje"].ToString(),
                archivo = (resultadoGeneracion["Archivo"] == null ? null : (byte[])resultadoGeneracion["Archivo"])
            };

            var vm = new ColaboradoresCVVm
            {
                Json = Newtonsoft.Json.JsonConvert.SerializeObject(resultado, Newtonsoft.Json.Formatting.Indented)
               //Json = resultado
            };

            return vm;
        }
    }
    public class ResultadoGeneracionVM
    {
        public int resultado { get; set; }
        public string mensaje { get; set; }
        public byte[] archivo { get; set; }

    }

}
