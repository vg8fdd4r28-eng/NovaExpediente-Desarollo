using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.AreasOrganizativas.Commands.UpsertAreaOrganizativa;
using NovaExpediente.Application.AreasOrganizativas.Commands.ValidaAreaOrganizativaDependencias;
using NovaExpediente.Application.AreasOrganizativas.Commands.DeleteAreaOrganizativa;
using NovaExpediente.Application.AreasOrganizativas.Queries.GetListaAreaOrganizativa;
using NovaExpediente.Application.AreasOrganizativas.Queries.GetAreaOrganizativaDetalle;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class AreaOrganizativaController : BaseController
    {

        [HttpGet(Name = "GetAreasOrganizativas")]
        
        public async Task<ActionResult<AreaOrganizativaListVm>> GetAreasOrganizativas()
        {
            return Ok(await Mediator.Send(new GetAreaOrganizativaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteAreaOrganizativaCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertAreaOrganizativaCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpGet(Name = "GetAreaOrganizativaDetalle")]
        
        public async Task<ActionResult<AreaOrganizativaDetalleVm>> GetAreaOrganizativaDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetAreaOrganizativaDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaAreaOrganizativaDependencias(ValidaAreaOrganizativaDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
