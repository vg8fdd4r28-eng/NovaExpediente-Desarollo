using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasCalidad.Queries.GetListaExperienciasCalidad;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaCalidadController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciaCalidadListVm>> GetExperienciaCalidad(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaCalidadListQuery { Id = id }));
        }

    }
}