using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasBPM.Queries.GetListaExperienciasBPM;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaBPMController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciaBPMListVm>> GetExperienciaBPM(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaBPMListQuery { Id = id }));
        }

    }
}