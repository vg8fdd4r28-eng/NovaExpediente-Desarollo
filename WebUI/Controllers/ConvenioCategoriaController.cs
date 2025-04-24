using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.AplicacionMovil.ConveniosCategorias.Commands.DeleteConveniosCategorias;
using NovaExpediente.Application.AplicacionMovil.ConveniosCategorias.Commands.UpsertConveniosCategorias;
using NovaExpediente.Application.AplicacionMovil.ConveniosCategorias.Queries.GetConveniosCategorias;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ConvenioCategoriaController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ConveniosCategoriasVm>> GetConveniosCategorias()
        {
            return Ok(await Mediator.Send(new GetConveniosCategoriasQuery()));
        }

        [HttpPost(Name = "UpsertConveniosCategoriasCommand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertConveniosCategoriasCommand(UpsertConveniosCategoriasCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await Mediator.Send(new DeleteConveniosCategoriasCommand { Id = id });
            return NoContent();
        }
    }
}
