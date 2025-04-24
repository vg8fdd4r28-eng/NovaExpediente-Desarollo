using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.CertificacionesProveedor.Queries.GetListaProveedorXCertificacion;
using NovaExpediente.Application.CertificacionesProveedor.Commands.ValidaCertificacionProveedorDependencias;
using NovaExpediente.Application.CertificacionesProveedor.Commands.ValidaProveedorCertificacionDependencias;

using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CertificacionProveedorController : BaseController
    {
        [HttpGet(Name = "GetProveedoresXCertificacion")]
        
        public async Task<ActionResult<CertificacionProveedorXCertificacionListVm>> GetProveedoresXCertificacion(int id)
        {
            return Ok(await Mediator.Send(new GetCertificacionProveedorXCertificacionListQuery { Id = id }));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaCertificacionProveedorDependencias(ValidaCertificacionProveedorDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaProveedorCertificacionDependencias(ValidaProveedorCertificacionDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
