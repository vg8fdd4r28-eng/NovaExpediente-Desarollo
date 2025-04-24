using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Provincias.Commands.DeleteProvincia;
using NovaExpediente.Application.Provincias.Commands.UpsertProvincia;
using NovaExpediente.Application.Provincias.Queries.GetListaProvincia;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ProvinciaController :BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<ProvinciaListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProvinciaListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertProvinciaCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProvinciaCommand { Id = id });

            return NoContent();
        }
    }
}
