using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.AplicacionMovil.Convenios.Commands.DeleteConvenios;
using NovaExpediente.Application.AplicacionMovil.Convenios.Commands.DeleteImagesConvenios;
using NovaExpediente.Application.AplicacionMovil.Convenios.Commands.UpsertConvenios;
using NovaExpediente.Application.AplicacionMovil.Convenios.Queries.GetDetalleConvenio;
using NovaExpediente.Application.AplicacionMovil.Convenios.Queries.GetListaConvenios;
using NovaExpediente.Application.Convenios.Queries.GetConvenioDetalleAPP;
using NovaExpediente.Application.Convenios.Queries.GetConveniosAPP;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ConvenioController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetConveniosAPP(int IdIdioma, int IdCategoria)
        {
            return Ok(await Mediator.Send(new GetConveniosAPPQuery { IdIdioma = IdIdioma, IdCategoria = IdCategoria }));
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetConvenioDetalleAPP(int Id)
        {
            return Ok(await Mediator.Send(new GetConvenioDetalleAPPQuery { Id = Id }));
        }



        [HttpGet]
        public async Task<ActionResult<ListaConveniosVm>> GetConvenios()
        {
            return Ok(await Mediator.Send(new GetListaConveniosQuery()));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertConvenios(UpsertConveniosCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetDetalleConvenio(int IdConvenio)
        {
            return Ok(await Mediator.Send(new GetDetalleConvenioQuery { IdConvenio = IdConvenio }));
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteConvenio(int id)
        {
            var res = await Mediator.Send(new DeleteConveniosCommand { Id = id });
            return Ok(res);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteImagesConvenios(int IdConvenio, int IdImagen)
        {
            var res = await Mediator.Send(new DeleteImagesConveniosCommand { IdConvenio = IdConvenio, IdImagen = IdImagen });
            return Ok(res);
        }



    }
}