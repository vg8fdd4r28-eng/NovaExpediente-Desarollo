using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ProyectoClasificadores.Queries.GetListaProyectoClasificador;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ProyectoClasificadorController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ProyectoClasificadorListVm>> GetProyectoClasificador(int id)
        {
            return Ok(await Mediator.Send(new GetProyectoClasificadorListQuery { Id = id }));
        }

    }
}
