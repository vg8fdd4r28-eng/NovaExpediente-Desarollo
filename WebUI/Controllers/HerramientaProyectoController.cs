using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.HerramientasProyectos.Queries.GetListaHerramientaProyecto;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class HerramientaProyectoController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<HerramientaProyectoListVm>> GetHerramientaProyecto(int id)
        {
            return Ok(await Mediator.Send(new GetHerramientaProyectoListQuery { Id = id }));
        }

    }
}
