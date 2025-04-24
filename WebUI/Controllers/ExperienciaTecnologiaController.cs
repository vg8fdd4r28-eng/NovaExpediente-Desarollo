using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasTecnologias.Queries.GetListaExperienciasTecnologias;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaTecnologiaController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciaTecnologiaListVm>> GetExperienciaTecnologia(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaTecnologiaListQuery { Id = id }));
        }

    }
}