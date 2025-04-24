using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.MasterDataTipoSolicitud.Queries.GetListaTipoSolicitudes;
using NovaExpediente.Application.MasterDataTipoSolicitud.Commands.DeleteTipoSolicitud;
using NovaExpediente.Application.MasterDataTipoSolicitud.Commands.UpsertTipoSolicitud;
using NovaExpediente.Application.MasterDataTipoSolicitud.Queries.GetDetalleTipoSolicitud;

using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class TipoSolicitudController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ListTipoSolicitudVm>> GetTiposSolicitudes()
        {
            return Ok(await Mediator.Send(new GetTipoSolicitudesListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertTipoSolicitudCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTipoSolicitudCommand { Id = id });

            return NoContent();
        }

        [HttpGet(Name = "GetTipoSolicitudDetalle")]
        
        public async Task<ActionResult<TipoSolicitudDetalleVm>> GetTipoSolicitudDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetTipoSolicitudDetalleListQuery { Id = id }));
        }



    }
}
