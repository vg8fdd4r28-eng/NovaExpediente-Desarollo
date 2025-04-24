using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.BPMS.Queries.GetBPMDetalle;
using NovaExpediente.Application.BPMS.Queries.GetListaBPMConsulta;
using NovaExpediente.Application.BPMS.Queries.GetListaBPM;
using NovaExpediente.Application.BPMS.Commands.DeleteBPM;
using NovaExpediente.Application.BPMS.Commands.UpsertBPM;
using NovaExpediente.Application.BPMS.Commands.ValidaBPMDependencias;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class BPMController : BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<BPMListVm>> GetBPMS()
        {
            return Ok(await Mediator.Send(new GetBPMListQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<BPMConsultaListVm>> GetBPMConsulta()
        {
            return Ok(await Mediator.Send(new GetBPMConsultaListQuery()));
        }


        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBPMCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertBPMCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpGet(Name = "GetBPMSDetalle")]
        
        public async Task<ActionResult<BPMDetalleVm>> GetBPMSDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetBPMDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaBPMDependencias(ValidaBPMDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


    }
}
