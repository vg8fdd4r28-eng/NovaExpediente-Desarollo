using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Proyectos.Queries.GetListaProyectos;
using NovaExpediente.Application.Proyectos.Queries.GetProyectoDetalle;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    // [SwaggerTag("Colaborador - Proyectos")]

    [Authorize]
    public class ProyectoController : BaseController
    {
        [HttpGet(Name = "GetProyectos")]
        
        public async Task<ActionResult<ProyectoListVm>> GetProyectos()
        {
            return Ok(await Mediator.Send(new GetProyectoListQuery ()));
        }


        [HttpGet(Name = "GetProyectoDetalle")]
        
        public async Task<ActionResult<ProyectoDetalleVm>> GetProyectoDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetProyectoDetalleListQuery { Id = id }));
        }

    }
}