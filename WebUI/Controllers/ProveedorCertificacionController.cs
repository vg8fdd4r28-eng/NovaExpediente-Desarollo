using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ProveedorCerficaciones.Commands.UpsertProveedorCertificacion;
using NovaExpediente.Application.ProveedorCerficaciones.Commands.ValidaProveedorDependencias;
using NovaExpediente.Application.ProveedorCerficaciones.Commands.DeleteProveedorCertificacion;
using NovaExpediente.Application.ProveedorCerficaciones.Queries.GetListaProveedorCertificaciones;
using NovaExpediente.Application.ProveedorCerficaciones.Queries.GetListaProveedorCertificacionesList;
using NovaExpediente.Application.ProveedorCerficaciones.Queries.GetProveedorCertificacionesDetalle;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ProveedorCertificacionController : BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<ProveedorCertificacionesListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProveedorCertificacionesListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProveedorCertificacionCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertProveedorCertificacionCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet]
        
        public async Task<ActionResult<ProveedorCertificacionesDetalleVm>> GetDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetProveedorCertificacionesDetalleListQuery { Id = id }));
        }


        [HttpGet]
        
        public async Task<ActionResult<ProveedorCertificacionesListaListVm>> GetProveedorCertificacionesLista()
        {
            return Ok(await Mediator.Send(new GetProveedorCertificacionesListaListQuery()));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaProveedorDependencias(ValidaProveedorDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
