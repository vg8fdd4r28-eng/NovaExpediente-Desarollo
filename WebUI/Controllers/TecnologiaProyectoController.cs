using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.TecnologiasProyectos.Queries.GetListaTecnologiaProyecto;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class TecnologiaProyectoController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<TecnologiaProyectoListVm>> GetTecnologiaProyecto(int id)
        {
            return Ok(await Mediator.Send(new GetTecnologiaProyectoListQuery { Id = id }));
        }

    }
}
