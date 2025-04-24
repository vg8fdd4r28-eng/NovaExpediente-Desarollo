using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Distritos.Commands.DeleteDistrito;
using NovaExpediente.Application.Distritos.Commands.UpsertDistrito;
using NovaExpediente.Application.Distritos.Queries.GetListaDistrito;
using NovaExpediente.Application.Distritos.Queries.GetDistritoDetalle;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class DistritoController :BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<DistritoListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetDistritoListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertDistritoCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteDistritoCommand { Id = id });

            return NoContent();
        }

        [HttpGet(Name = "GetDistritoDetalle")]
        
        public async Task<ActionResult<DistritoDetalleVm>> GetDistritoDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetDistritoDetalleListQuery { Id = id }));
        }


    }
}
