using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ProyectoLenguajes.Queries.GetListaProyectoLenguaje;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ProyectoLenguajeController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ProyectoLenguajeListVm>> GetProyectoLenguaje(int id)
        {
            return Ok(await Mediator.Send(new GetProyectoLenguajeListQuery { Id = id }));
        }

    }
}
