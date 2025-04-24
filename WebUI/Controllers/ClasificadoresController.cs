using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Clasificadores.Commands.DeleteClasificador;
using NovaExpediente.Application.Clasificadores.Commands.UpsertClasificador;
using NovaExpediente.Application.Clasificadores.Queries.GetListaClasificadores;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ClasificadoresController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ClasificadoresListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetClasificadoresListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertClasificadorCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteClasificadorCommand { Id = id });

            return NoContent();
        }
    }
}
