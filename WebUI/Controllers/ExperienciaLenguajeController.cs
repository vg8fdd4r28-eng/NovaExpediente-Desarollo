using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasLenguajes.Queries.GetListaExperienciasLenguajes;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaLenguajeController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciaLenguajeListVm>> GetExperienciaLenguaje(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaLenguajeListQuery { Id = id }));
        }

    }
}