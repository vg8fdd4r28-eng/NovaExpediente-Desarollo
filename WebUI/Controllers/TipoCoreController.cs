using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.TiposCore.Queries.GetListaTiposCore;
using NovaExpediente.Application.TiposCore.Queries.GetTipoCoreDetalle;
using NovaExpediente.Application.TiposCore.Commands.UpsertTipoCore;
using NovaExpediente.Application.TiposCore.Commands.DeleteTipoCore;
using NovaExpediente.Application.TiposCore.Commands.ValidaTipoCoreDependencias;

using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class TipoCoreController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<TipoCoreListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetTipoCoreListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTipoCoreCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertTipoCoreCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet]
        
        public async Task<ActionResult<TipoCoreDetalleVm>> GetDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetTipoCoreDetalleListQuery { Id = id }));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaTipoCoreDependencias(ValidaTipoCoreDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



    }
}
