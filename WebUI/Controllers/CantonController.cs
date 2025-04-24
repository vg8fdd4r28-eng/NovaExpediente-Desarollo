using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Cantones.Commands.DeleteCanton;
using NovaExpediente.Application.Cantones.Commands.UpsertCanton;
using NovaExpediente.Application.Cantones.Queries.GetListaCanton;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CantonController :BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<CantonListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCantonListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertCantonCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCantonCommand { Id = id });

            return NoContent();
        }
    }
}
