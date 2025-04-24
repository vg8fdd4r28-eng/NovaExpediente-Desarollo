using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Afiliacion.Queries.AfiliacionDetalle;
using NovaExpediente.Application.Afiliacion.Queries.GetCertificado;
using NovaExpediente.Application.Afiliacion.Queries.GetListaAfiliacion;
using NovaExpediente.Application.Afiliacion.Queries.GetListaAfiliacionDetalle;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    public class AfiliacionController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<AfiliacionListVm>> GetByColaborador(int id)
        {
            return Ok(await Mediator.Send(new GetAfiliacionListQuery { Id = id }));
        }

        [HttpGet]
        
        public async Task<ActionResult<AfiliacionDetalleVm>> GetDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetAfiliacionDetalleListQuery { Id = id }));
        }

        [HttpGet(Name = "GetCertificadoAfiliacionById")]
        
        public async Task<ActionResult<CertificadoAfiliacionVm>> GetCertificadoAfiliacionById(int id)
        {
            return Ok(await Mediator.Send(new GetCertificadoByIdQuery { Id = id }));
        }
    }
}
