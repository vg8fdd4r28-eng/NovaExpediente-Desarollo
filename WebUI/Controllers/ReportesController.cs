using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.FuncionesAdministrativas.Queries.ReporteCertificacionesColaborador;
using NovaExpediente.Application.FuncionesAdministrativas.Queries.ReporteCursosColaborador;
using NovaExpediente.Application.FuncionesAdministrativas.Queries.ReporteFormacionColaborador;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ReportesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetReporteCertificacionesColaborador()
        {
            return Ok(await Mediator.Send(new GetReporteCertificacionesColaboradorQuery()));
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetReporteCursosColaborador()
        {
            return Ok(await Mediator.Send(new GetReporteCursosColaboradorQuery()));
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetReporteFormacionColaborador()
        {
            return Ok(await Mediator.Send(new GetReporteFormacionColaboradorQuery()));
        }

    }
}
