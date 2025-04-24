using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Paises.Commands.DeletePais;
using NovaExpediente.Application.Paises.Commands.UpsertPais;
using NovaExpediente.Application.Paises.Queries.GetListaPais;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class PaisController :BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<PaisListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetPaisListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertPaisCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePaisCommand { Id = id });

            return NoContent();
        }
    }
}
