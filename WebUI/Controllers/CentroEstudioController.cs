using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.CentrosEstudios.Commands.DeleteCentroEstudio;
using NovaExpediente.Application.CentrosEstudios.Commands.UpsertCentroEstudio;
using NovaExpediente.Application.CentrosEstudios.Commands.ValidaCentroEstudioDependencias;
using NovaExpediente.Application.CentrosEstudios.Queries.GetListaCentroEstudio;
using NovaExpediente.Application.CentrosEstudios.Queries.GetCentroEstudioDetalle;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CentroEstudioController : BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<CentroEstudioListVm>> GetCentrosEstudio()
        {
            return Ok(await Mediator.Send(new GetCentroEstudioListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCentroEstudioCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertCentroEstudioCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpGet(Name = "GetCentrosEstudioDetalle")]
        
        public async Task<ActionResult<CentroEstudioDetalleVm>> GetCentrosEstudioDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetCentroEstudioDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaCentroEstudioDependencias(ValidaCentroEstudioDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



    }
}
