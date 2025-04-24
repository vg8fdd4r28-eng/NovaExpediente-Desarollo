using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.AplicacionMovil.Noticias.Queries.GetListaNoticias;
using NovaExpediente.Application.AplicacionMovil.Noticias.Commands.UpsertNoticias;
using NovaExpediente.Application.AplicacionMovil.Noticias.Commands.DeleteNoticias;
using NovaExpediente.Application.AplicacionMovil.Noticias.Commands.DeleteImagesNoticias;
using NovaExpediente.Application.AplicacionMovil.Noticias.Queries.GetDetalleNoticia;
using NovaExpediente.Application.Noticias.Queries.GetNoticiasAPP;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class NoticiaController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<string>> GetNoticiasPublicasAPP(int IdIdioma)
        {
            return Ok(await Mediator.Send(new GetNoticiasAPPQuery { EsPublica = true, IdIdioma = IdIdioma }));
        }



        [HttpGet]
        public async Task<ActionResult<string>> GetNoticiasPrivadasAPP(int IdIdioma)
        {
            return Ok(await Mediator.Send(new GetNoticiasAPPQuery { EsPublica = false, IdIdioma = IdIdioma }));
        }


        [HttpGet]
        public async Task<ActionResult<ListaNoticiasVm>> GetNoticias()
        {
            return Ok(await Mediator.Send(new GetListaNoticiasQuery()));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertNoticias(UpsertNoticiasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetDetalleNoticia(int IdNoticia)
        {
            return Ok(await Mediator.Send(new GetDetalleNoticiaQuery { IdNoticia = IdNoticia }));
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await Mediator.Send(new DeleteNoticiasCommand { Id = id });
            return Ok(res);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteImagesNoticias(int IdNoticia, int IdImagen)
        {
            var res = await Mediator.Send(new DeleteImagesNoticiasCommand { IdNoticia = IdNoticia, IdImagen = IdImagen });
            return Ok(res);
        }


    }
}