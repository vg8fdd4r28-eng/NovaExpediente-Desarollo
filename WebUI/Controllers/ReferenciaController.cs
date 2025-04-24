using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Referencias.Queries.GetListaReferencias;
using NovaExpediente.Application.Referencias.Queries.GetReferenciasDetalle;
using NovaExpediente.Application.Referencias.Queries.GetReferenciasXCliente;
using NovaExpediente.Application.Referencias.Commands.UpsertReferencia;
using NovaExpediente.Application.Referencias.Commands.DeleteReferencia;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NovaExpediente.WebUI.Controllers
{


    [Authorize]
    public class ReferenciaController : BaseController
    {
        [HttpGet(Name = "GetReferencias")]
        
        public async Task<ActionResult<ReferenciaListVm>> GetReferencias()
        {
            return Ok(await Mediator.Send(new GetReferenciaListQuery ()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteReferenciaCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertReferenciaCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet]
        
        public async Task<ActionResult<ReferenciaDetalleVm>> GetReferenciasDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetReferenciaDetalleListQuery { Id = id }));
        }


        [HttpGet]
        
        public async Task<ActionResult<ReferenciaXClienteListVm>> GetReferenciasXCliente(int id)
        {
            return Ok(await Mediator.Send(new GetReferenciaXClienteListQuery { Id = id }));
        }


    }
}