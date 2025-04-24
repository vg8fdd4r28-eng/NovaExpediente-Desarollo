using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Denuncias.Commands.IngresoDenuncias;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class DenunciaController : BaseController
    {

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> IngresoDenunciasAPP(IngresoDenunciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }
    }
}
