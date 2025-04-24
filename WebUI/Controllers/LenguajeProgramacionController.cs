using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.LenguajesProgramacion.Queries.GetListaLenguajeProgramacion;
using NovaExpediente.Application.LenguajesProgramacion.Queries.GetLenguajeProgramacionDetalle;
using NovaExpediente.Application.LenguajesProgramacion.Queries.GetListaLenguajeProgramacionConsulta;
using NovaExpediente.Application.LenguajesProgramacion.Commands.UpsertLenguajesProgramacion;
using NovaExpediente.Application.LenguajesProgramacion.Commands.DeleteLenguajesProgramacion;
using NovaExpediente.Application.LenguajesProgramacion.Commands.ValidaLenguajeDependencias;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class LenguajeProgramacionController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<LenguajeProgramacionListVm>> GetLenguajeProgramacion()
        {
            return Ok(await Mediator.Send(new GetLenguajeProgramacionListQuery()));
        }

        [HttpGet(Name = "GetLenguajeProgramacionConsulta")]
        
        public async Task<ActionResult<LenguajeProgramacionConsultaListVm>> GetLenguajeProgramacionConsulta()
        {
            return Ok(await Mediator.Send(new GetLenguajeProgramacionConsultaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteLenguajesProgramacionCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertLenguajesProgramacionCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet(Name = "GetLenguajeProgramacionDetalle")]
        
        public async Task<ActionResult<LenguajeProgramacionDetalleVm>> GetLenguajeProgramacionDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetLenguajeProgramacionDetalleListQuery { Id = id }));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaLenguajeDependencias(ValidaLenguajeDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
