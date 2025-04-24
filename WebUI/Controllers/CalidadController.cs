using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidad;
using NovaExpediente.Application.HerramientasCalidad.Queries.GetCalidadDetalle;
using NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidadConsulta;
using NovaExpediente.Application.HerramientasCalidad.Commands.DeleteCalidad;
using NovaExpediente.Application.HerramientasCalidad.Commands.UpsertCalidad;
using NovaExpediente.Application.HerramientasCalidad.Commands.ValidaCalidadDependencias;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CalidadController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<CalidadListVm>> GetCalidad()
        {
            return Ok(await Mediator.Send(new GetCalidadListQuery()));
        }

        [HttpGet(Name = "GetCalidadConsulta")]
        
        public async Task<ActionResult<CalidadConsultaListVm>> GetCalidadConsulta()
        {
            return Ok(await Mediator.Send(new GetCalidadConsultaListQuery()));
        }


        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCalidadCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertCalidadCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpGet(Name = "GetCalidadDetalle")]
        
        public async Task<ActionResult<CalidadDetalleVm>> GetCalidadDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetCalidadDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaCalidadDependencias(ValidaCalidadDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
