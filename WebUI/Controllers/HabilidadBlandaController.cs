using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.HablilidadesBlandas.Commands.UpsertHabilidadBlanda;
using NovaExpediente.Application.HablilidadesBlandas.Commands.DeleteHabilidadBlanda;
using NovaExpediente.Application.HablilidadesBlandas.Queries.GetListaHabilidadesBlandas;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class HabilidadBlandaController : BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<HabilidadesBlandasListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetHabilidadesBlandasListQuery()));
        }


        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteHabilidadBlandaCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertHabilidadBlandaCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }
    }
}
