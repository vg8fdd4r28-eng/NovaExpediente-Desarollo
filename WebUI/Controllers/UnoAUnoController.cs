using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.UnosAUnos.Queries.GetListaUnosAUnos;
using NovaExpediente.Application.UnosAUnos.Queries.GetDetalleUnoAUno;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NovaExpediente.Application.UnosAUnos.Commands.UpsertUnosAUnos;
using NovaExpediente.Application.UnosAUnos.Commands.UpsertUnosaUnosDetalles;
using NovaExpediente.Application.UnosAUnos.Commands.DeleteUnoaUnoDetalle;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class UnoAUnoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ListaUnosAUnosVm>> GetUnosAUnos(int IdDeliveryManager)
        {
            return Ok(await Mediator.Send(new GetListaUnosAUnosQuery { IdDeliveryManager = IdDeliveryManager }));
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetDetalleUnoAUno(int IdUnoAUno)
        {
            return Ok(await Mediator.Send(new GetDetalleUnoAUnoQuery { IdUnoAUno = IdUnoAUno }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertUnosAUnos(UpsertUnosAUnosCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertUnosAUnosDetalles(UpsertUnosAUnosDetallesCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDetalleUnoAUno(int id)
        {
            var res = await Mediator.Send(new DeleteUnoaUnoDetalleCommand { Id = id });
            return Ok(res);
        }
    }

}