using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.CursosColaborador.Queries.GetCursoColaboradorDetalle;
using NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaborador;
using NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaboradorAdjunto;
using NovaExpediente.Application.CursosColaborador.Queries.GetCursoColaboradorDocumento;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [SwaggerTag("Colaborador - Cursos")]

    [Authorize]
    public class CursoColaboradorController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<CursoColaboradorListVm>> GetCursosColaborador(int id)
        {
            return Ok(await Mediator.Send(new GetCursoColaboradorListQuery { Id = id }));
        }



        [HttpGet]
        
        public async Task<ActionResult<CursoColaboradorDetalleVm>> GetCursoColaboradorDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetCursoColaboradorDetalleListQuery { Id = id }));
        }


        [HttpGet]
        
        public async Task<ActionResult<CursoColaboradorAdjuntoListVm>> GetCursosColaboradorAdjunto(int id)
        {
            return Ok(await Mediator.Send(new GetCursoColaboradorAdjuntoListQuery { Id = id }));
        }


        #region Obtener certificado 
        [HttpGet(Name = "GetCursoColaboradorDocumentoById")]
        
        public async Task<ActionResult<CursoColaboradorDocumentoVm>> GetCursoColaboradorDocumentoById(int id)
        {
            return Ok(await Mediator.Send(new GetCursoColaboradorDocumentoByIdQuery { Id = id }));
        }
        #endregion


    }
}