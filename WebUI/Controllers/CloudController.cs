using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Clouds.Commands.ValidaCloudDependencias;
using NovaExpediente.Application.Clouds.Queries.GetListaCloud;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CloudController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<CloudListVm>> GetCloud()
        {
            return Ok(await Mediator.Send(new GetCloudListQuery()));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaCloudDependencias(ValidaCloudDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }

}
