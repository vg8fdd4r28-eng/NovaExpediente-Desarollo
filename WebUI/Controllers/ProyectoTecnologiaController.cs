using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ProyectoTecnologias.Queries.GetListaProyectoTecnologia;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ProyectoTecnologiaController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ProyectoTecnologiaListVm>> GetProyectoTecnologia(int id)
        {
            return Ok(await Mediator.Send(new GetProyectoTecnologiaListQuery { Id = id }));
        }

    }
}
