using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasInfraestructura.Queries.GetListaExperienciasInfraestructura;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaInfraestructuraController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciaInfraestructuraListVm>> GetExperienciaInfraestructura(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaInfraestructuraListQuery { Id = id }));
        }

    }
}