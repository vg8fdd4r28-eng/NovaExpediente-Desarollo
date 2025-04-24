using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaborador;
using NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaboradorAdjunto;
using NovaExpediente.Application.CertificacionesColaborador.Queries.GetCertificacionColaboradorDetalle;
using NovaExpediente.Application.CertificacionesColaborador.Queries.GetCertificacionDocumento;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace NovaExpediente.WebUI.Controllers
{
    [SwaggerTag("Colaborador - Certificaciones")]

    [Authorize]
    public class CertificacionColaboradorController :  BaseController
    {
        [HttpGet(Name = "GetCertificacionesColaborador")]
        
    public async Task<ActionResult<CertificacionColaboradorListVm>> GetCertificacionesColaborador(int id)
    {
        return Ok(await Mediator.Send(new GetCertificacionColaboradorListQuery { Id = id }));
    }

        [HttpGet(Name = "GetCertificacionesColaboradorAdjunto")]
        
        public async Task<ActionResult<CertificacionColaboradorAdjuntoListVm>> GetCertificacionesColaboradorAdjunto(int id)
        {
            return Ok(await Mediator.Send(new GetCertificacionColaboradorAdjuntoListQuery { Id = id }));
        }

        [HttpGet(Name = "GetCertificacionColaboradorDetalle")]
        
        public async Task<ActionResult<CertificacionColaboradorDetalleVm>> GetCertificacionColaboradorDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetCertificacionColaboradorDetalleListQuery { Id = id }));
        }

        #region Obtener certificado 
        [HttpGet(Name = "GetCertificacionDocumentoById")]
        
        public async Task<ActionResult<CertificacionDocumentoVm>> GetCertificacionDocumentoById(int id)
        {
            return Ok(await Mediator.Send(new GetCertificacionDocumentoByIdQuery { Id = id }));
        }
        #endregion



    }
}
