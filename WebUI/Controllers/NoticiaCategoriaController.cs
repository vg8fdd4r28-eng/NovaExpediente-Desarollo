using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Queries.GetNoticiasCategorias;
using NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Commands.UpsertNoticiasCategorias;
using NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Commands.DeleteNoticiasCategorias;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class NoticiaCategoriaController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<NoticiasCategoriasVm>> GetNoticiasCategorias()
        {
            return Ok(await Mediator.Send(new GetNoticiasCategoriasQuery()));
        }

        [HttpPost(Name = "Upsert")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertNoticiasCategoriasCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
           var res= await Mediator.Send(new DeleteNoticiasCategoriasCommand { Id = id });
            return Ok(res);
        }
    }
}
