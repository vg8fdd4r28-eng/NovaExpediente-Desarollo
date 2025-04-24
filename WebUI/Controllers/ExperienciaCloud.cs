using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasCloud.Queries.GetListaExperienciasCloud;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaCloud : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciasCloudListVm>> GetExperienciaCloud(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciasCloudListQuery { Id = id }));
        }
    }
}
