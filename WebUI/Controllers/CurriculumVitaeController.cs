using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.CurriculumVitae.Commands;
using NovaExpediente.Application.CurriculumVitae.Queries;
using NovaExpediente.Application.CurriculumVitae.Queries.GetCurriculumVitae;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CurriculumVitaeController : BaseController
    {
        public CurriculumVitaeController()
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CargarCV([FromForm] IFormFile data, [FromForm] int idColaborador, [FromForm] string Json)
        {
            var id = await Mediator.Send(new CargarCVCommand { Json = Json, nombreArchivo = data.FileName,  idUsuarioCrea = idColaborador});
            return Ok(id);
        }

        [HttpGet(Name = "GetCurriculumVitae")]
        public async Task<ActionResult<CurriculumVitaeListVm>> GetCurriculumVitae(int IdColaborador)
        {
            return Ok(await Mediator.Send(new GetCurriculumVitaeQuery { IdColaborador = IdColaborador }));
        }

    }
}
