using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.OneSignalAPP.Queries.GetDevices;
using NovaExpediente.Application.OneSignalAPP.Queries.GetNotifications;
using NovaExpediente.Application.OneSignalAPP.Commands.DeleteDevice;
using NovaExpediente.Application.OneSignalAPP.Commands.SendNotification;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{

    [Authorize]
    public class OneSignalController : BaseController
    {



        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetDevices()
        {

            return Ok(await Mediator.Send(new GetDevicesListQuery()));

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetNotifications(string cliente)
        {
            return Ok(await Mediator.Send(new GetNotificationsListQuery { cliente = cliente}));
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteDeviceCommand { Id_Player = id });

            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SendNotification(SendNotificationCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



    }
}
