using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Sectores.Queries.GetListaSectores;
using NovaExpediente.Application.Sectores.Queries.GetSectoresDetalle;
using NovaExpediente.Application.Sectores.Commands.UpsertSector;
using NovaExpediente.Application.Sectores.Commands.DeleteSector;
using NovaExpediente.Application.Sectores.Commands.ValidaSectorDependencias;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class SectorController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<SectorListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetSectorListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSectorCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertSectorCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet]
        
        public async Task<ActionResult<SectorDetalleVm>> GetDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetSectorDetalleListQuery { Id = id }));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaSectorDependencias(ValidaSectorDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


    }
}
