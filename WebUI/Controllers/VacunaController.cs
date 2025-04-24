
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Vacunas.Commands.DeleteVacuna;
using NovaExpediente.Application.Vacunas.Queries.GetListaVacuna;
using NovaExpediente.Application.Entidad.Commands.UpsertEntidad;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    //[Authorize]
    public class VacunaController : BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<VacunaListVm>> GetVacunaList()
        {
            return Ok(await Mediator.Send(new GetVacunaListQuery()));
        }

        /*
        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertVacunaCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await Mediator.Send(new DeleteVacunaCommand { Id = id });

            return NoContent();
        }
        */

    }
}
