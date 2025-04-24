using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Puestos.Commands.DeletePuesto;
using NovaExpediente.Application.Puestos.Commands.UpsertPuesto;
using NovaExpediente.Application.Puestos.Commands.ValidaPuestoDependencias;
using NovaExpediente.Application.Puestos.Queries.GetListaPuesto;
using NovaExpediente.Application.Puestos.Queries.GetPuestoDetalle;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class PuestoController :BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<PuestoListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetPuestoListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertPuestoCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePuestoCommand { Id = id });

            return NoContent();
        }


        [HttpGet]
        
        public async Task<ActionResult<PuestoDetalleVm>> GetDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetPuestoDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaPuestoDependencias(ValidaPuestoDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


    }
}
