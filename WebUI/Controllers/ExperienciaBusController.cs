using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasBus.Queries.GetListaExperienciasBus;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaBusController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciaBusListVm>> GetExperienciaBus(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaBusListQuery { Id = id }));
        }

    }
}