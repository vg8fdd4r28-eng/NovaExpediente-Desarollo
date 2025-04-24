using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasERP.Queries.GetListaExperienciasERP;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaERPController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciaERPListVm>> GetExperienciaERP(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaERPListQuery { Id = id }));
        }

    }
}