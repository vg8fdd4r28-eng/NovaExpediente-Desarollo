using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Buses.Queries.GetListaBusConsulta;
using NovaExpediente.Application.Buses.Queries.GetListaBus;
using NovaExpediente.Application.Buses.Queries.GetBusDetalle;
using NovaExpediente.Application.Buses.Commands.DeleteBus;
using NovaExpediente.Application.Buses.Commands.UpsertBus;
using NovaExpediente.Application.Buses.Commands.ValidaBusDependencias;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class BusController : BaseController
    {
        [HttpGet(Name = "GetBus")]
        
        public async Task<ActionResult<BusListVm>> GetBus()
        {
            return Ok(await Mediator.Send(new GetBusListQuery()));
        }

        [HttpGet(Name = "GetBusConsulta")]
        
        public async Task<ActionResult<BusConsultaListVm>> GetBusConsulta()
        {
            return Ok(await Mediator.Send(new GetBusConsultaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBusCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertBusCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet(Name = "GetBusDetalle")]
        
        public async Task<ActionResult<BusDetalleVm>> GetBusDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetBusDetalleListQuery { Id = id }));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaBusDependencias(ValidaBusDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }





}
