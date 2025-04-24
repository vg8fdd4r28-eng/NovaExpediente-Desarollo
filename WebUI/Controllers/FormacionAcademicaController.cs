using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademica;
using NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaAdjunto;
using NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaDocumento;
using NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaDetalle;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class FormacionAcademicaController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<FormacionAcademicaListVm>> GetByColaborador(int id)
        {
            return Ok(await Mediator.Send(new GetFormacionAcademicaListQuery { Id = id }));
        }

        [HttpGet]
        
        public async Task<ActionResult<FormacionAcademicaDetalleVm>> GetDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetFormacionAcademicaDetalleListQuery { Id = id }));
        }



        [HttpGet]
        
        public async Task<ActionResult<FormacionAcademicaAdjuntoListVm>> GetFormacionAcademicaAdjunto(int id)
        {
            return Ok(await Mediator.Send(new GetFormacionAcademicaAdjuntoListQuery { Id = id }));
        }


        #region Obtener certificado 
        [HttpGet(Name = "GetFormacionAcademicaDocumentoById")]
        
        public async Task<ActionResult<FormacionAcademicaDocumentoVm>> GetFormacionAcademicaDocumentoById(int id)
        {
            return Ok(await Mediator.Send(new GetFormacionAcademicaDocumentoByIdQuery { Id = id }));
        }
        #endregion



    }
}
