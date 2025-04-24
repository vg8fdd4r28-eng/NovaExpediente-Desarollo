using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Idiomas.Commands.Delete;
using NovaExpediente.Application.Idiomas.Commands.UpsertIdiomas;
using NovaExpediente.Application.Idiomas.Commands.ValidaIdiomaDependencias;
using NovaExpediente.Application.Idiomas.Queries.GetListaIdiomas;
using NovaExpediente.Application.Idiomas.Queries.GetIdiomasDetalle;

using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class IdiomasController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<IdiomaListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetIdiomaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteIdiomasCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertIdiomasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet(Name = "GetDetalle")]
        
        public async Task<ActionResult<IdiomaDetalleVm>> GetDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetIdiomaDetalleListQuery { Id = id }));
        }



        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaIdiomaDependencias(ValidaIdiomaDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
