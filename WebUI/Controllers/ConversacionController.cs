using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Conversaciones.Commands.InsertConversaciones;
using NovaExpediente.Application.Conversaciones.Commands.InsertConversacionMensajes;
using NovaExpediente.Application.Conversaciones.Queries.GetDetalleConversacion;
using NovaExpediente.Application.Conversaciones.Queries.GetListaConversaciones;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [SwaggerTag("Conversaciones")]
    [Authorize]
    public class ConversacionController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetListaConversaciones(string userId, int currentPage)
        {
            return Ok(await Mediator.Send(new GetListaConversacionesQuery{ userId = userId,currentPage = currentPage}));
        }

        [HttpGet]
        public async Task<IActionResult> GetListaDetalleConversacion(string conversationId)
        {
            return Ok(await Mediator.Send(new GetListaDetalleConversacionQuery { conversationId = conversationId }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> InsertConversaciones(InsertConversacionesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> InsertConversacionMensaje(InsertConversacionMensajeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


    }
}
