using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacion;
using NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacionProveedores;
using NovaExpediente.Application.Certificaciones.Queries.GetCertificacionDetalle;
using NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacionConsulta;
using NovaExpediente.Application.Certificaciones.Commands.DeleteCertificaciones;
using NovaExpediente.Application.Certificaciones.Commands.UpsertCertificaciones;
using NovaExpediente.Application.Certificaciones.Commands.ValidaCertificacionDependencias;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CertificacionController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<CertificacionListVm>> GetCertificacion()
        {
            return Ok(await Mediator.Send(new GetCertificacionListQuery()));
        }


        [HttpGet(Name = "GetCertificacionConsulta")]
        
        public async Task<ActionResult<CertificacionConsultaListVm>> GetCertificacionConsulta()
        {
            return Ok(await Mediator.Send(new GetCertificacionConsultaListQuery()));
        }

        [HttpGet(Name = "GetCertificacionProveedores")]
        
        public async Task<ActionResult<CertificacionProveedoresListVm>> GetCertificacionProveedores()
        {
            return Ok(await Mediator.Send(new GetCertificacionProveedoresListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertCertificacionesCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCertificacionesCommand { Id = id });

            return NoContent();
        }

        [HttpGet(Name = "GetCertificacionDetalle")]
        
        public async Task<ActionResult<CertificacionDetalleVm>> GetCertificacionDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetCertificacionDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaCertificacionDependencias(ValidaCertificacionDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
