using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ProyectoAmbientes.Queries.GetListaProyectoAmbientes;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ProyectoAmbienteController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ProyectoAmbienteListVm>> GetProyectoAmbiente(int id)
        {
            return Ok(await Mediator.Send(new GetProyectoAmbienteListQuery { Id = id }));
        }

    }
}
