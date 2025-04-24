using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Infraestructuras.Queries.GetListaInfraestructura;
using NovaExpediente.Application.Infraestructuras.Queries.GetInfraestructuraDetalle;
using NovaExpediente.Application.Infraestructuras.Queries.GetListaInfraestructuraConsulta;
using NovaExpediente.Application.Infraestructuras.Commands.UpsertInfraestructura;
using NovaExpediente.Application.Infraestructuras.Commands.DeleteInfraestructura;
using NovaExpediente.Application.Infraestructuras.Commands.ValidaInfraestructuraDependencias;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class InfraestructuraController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<InfraestructuraListVm>> GetInfraestructura()
        {
            return Ok(await Mediator.Send(new GetInfraestructuraListQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<InfraestructuraConsultaListVm>> GetInfraestructuraConsulta()
        {
            return Ok(await Mediator.Send(new GetInfraestructuraConsultaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteInfraestructuraCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertInfraestructuraCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet(Name = "GetInfraestructuraDetalle")]
        
        public async Task<ActionResult<InfraestructuraDetalleVm>> GetInfraestructuraDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetInfraestructuraDetalleListQuery { Id = id }));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaInfraestructuraDependencias(ValidaInfraestructuraDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
