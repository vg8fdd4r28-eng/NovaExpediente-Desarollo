using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.AplicacionMovil.Sorteos.Commands.DeleteImagesSorteo;
using NovaExpediente.Application.AplicacionMovil.Sorteos.Commands.InactivarSorteo;
using NovaExpediente.Application.AplicacionMovil.Sorteos.Commands.UpsertSorteo;
using NovaExpediente.Application.AplicacionMovil.Sorteos.Queries.GetDetalleSorteo;
using NovaExpediente.Application.AplicacionMovil.Sorteos.Queries.GetDetalleSorteoAPP;
using NovaExpediente.Application.AplicacionMovil.Sorteos.Queries.GetListaSorteos;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class SorteoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ListaSorteosVm>> GetListaSorteos()
        {
            return Ok(await Mediator.Send(new GetListaSorteosQuery()));
        }
        [HttpGet]
        public async Task<ActionResult<string>> GetDetalleSorteo(int idSorteo)
        {
           return Ok(await Mediator.Send(new GetDetalleSorteoQuery { IdSorteo = idSorteo }));
        }
        [HttpGet]
        public async Task<ActionResult<string>> GetDetalleSorteoAPP()
        {
            return Ok(await Mediator.Send(new GetDetalleSorteoAPPQuery()));
        }
        [HttpDelete("{idSorteo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int idSorteo)
        {
            var response = await Mediator.Send(new InactivarSorteoCommand { IdSorteo = idSorteo });
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertSorteo(UpsertSorteoCommand command)
        {
            var idSorteo = await Mediator.Send(command);
            return Ok(idSorteo);
        }

        [HttpDelete, ProducesResponseType(StatusCodes.Status204NoContent), ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteImagesSorteo(int idSorteo, int IdImagen)
        {
            var res = await Mediator.Send(new DeleteImagesSorteoCommand { IdSorteo = idSorteo, IdImagen = IdImagen });
            return Ok(res);
        }
    }
}
