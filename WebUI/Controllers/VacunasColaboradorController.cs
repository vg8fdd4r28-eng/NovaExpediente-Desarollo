
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.VacunasColaboradores.Commands.DeleteVacunasColaborador;
using NovaExpediente.Application.VacunasColaboradores.Queries.GetListaVacunasColaborador;
using NovaExpediente.Application.Entidad.Commands.UpsertEntidad;
using System.Threading.Tasks;
using NovaExpediente.Application.VacunasColaboradores.Queries.GetVacunasColaboradorDetalle;

namespace NovaExpediente.WebUI.Controllers
{
    //[Authorize]
    public class VacunasColaboradorController : BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<VacunasColaboradorListVm>> GetVacunasColaboradorList(int idColaborador)
        {
            return Ok(await Mediator.Send(new GetVacunasColaboradorListQuery { IdColaborador = idColaborador }));
        }

        /*
        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertVacunasColaboradorCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await Mediator.Send(new DeleteVacunasColaboradorCommand { Id = id });

            return NoContent();
        }
        */

    }
}
