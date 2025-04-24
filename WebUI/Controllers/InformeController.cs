using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Informes.Commands.InsertInformes;
using NovaExpediente.Application.Informes.Commands.InsertInformeMensaje;
using NovaExpediente.Application.Informes.Queries.GetDetalleInforme;
using NovaExpediente.Application.Informes.Queries.GetListaInformes;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NovaExpediente.WebUI.Controllers
{
    [SwaggerTag("InformesIA")]
    [Authorize]
    public class InformeController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetListaInformes(string userId, int currentPage) =>
            Ok(await Mediator.Send(
                new GetListaInformesQuery { UserId = userId, CurrentPage = currentPage }));

        [HttpGet]
        public async Task<IActionResult> GetListaDetalleInforme(string conversationId) =>
            Ok(await Mediator.Send(
                new GetDetalleInformeQuery { ConversationId = conversationId }));

        [HttpPost, ProducesResponseType(StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> InsertInformes(InsertInformesCommand command) =>
            Ok(await Mediator.Send(command));
        
        [HttpPost, ProducesResponseType(StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> InsertInformeMensaje(InsertInformeMensajeCommand command) =>
            Ok(await Mediator.Send(command));
    }
}
