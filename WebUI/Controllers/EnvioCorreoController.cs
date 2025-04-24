using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.EnvioCorreo.Commands;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class EnvioCorreoController : BaseController
    {
        [HttpPost(Name = "EnvioCorreo")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EnvioCorreo(EnvioCorreoCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }
    }
}
