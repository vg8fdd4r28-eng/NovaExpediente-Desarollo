using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador;
using NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorAdjunto;
using NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorDocumento;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [SwaggerTag("Colaborador - Idiomas")]

    [Authorize]
    public class IdiomaColaboradorController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<IdiomaColaboradorListVm>> GetIdiomasColaborador(int id)
        {
            return Ok(await Mediator.Send(new GetIdiomasColaboradorListQuery { Id = id }));
        }




        [HttpGet]
        
        public async Task<ActionResult<IdiomaColaboradorAdjuntoListVm>> GetIdiomaColaboradorAdjunto(int id)
        {
            return Ok(await Mediator.Send(new GetIdiomasColaboradorAdjuntoListQuery { Id = id }));
        }


        #region Obtener certificado 
        [HttpGet(Name = "GetIdiomaColaboradorDocumentoById")]
        
        public async Task<ActionResult<IdiomaColaboradorDocumentoVm>> GetIdiomaColaboradorDocumentoById(string id)
        {
            return Ok(await Mediator.Send(new GetIdiomasColaboradorDocumentoListQuery { Id = id }));
        }
        #endregion



    }
}
