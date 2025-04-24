using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ErpCores.Queries.GetListaErpCore;
using NovaExpediente.Application.ErpCores.Queries.GetErpCoreDetalle;
using NovaExpediente.Application.ErpCores.Queries.GetListaErpCoreConsulta;
using NovaExpediente.Application.ErpCores.Commands.UpsertErpCores;
using NovaExpediente.Application.ErpCores.Commands.DeleteErpCores;
using NovaExpediente.Application.ErpCores.Commands.ValidaERPDependencias;

using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ErpCoreController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ErpCoreListVm>> GetErpCore()
        {
            return Ok(await Mediator.Send(new GetErpCoreListQuery()));
        }

        [HttpGet(Name = "GetErpCoreConsulta")]
        
        public async Task<ActionResult<ErpCoreConsultaListVm>> GetErpCoreConsulta()
        {
            return Ok(await Mediator.Send(new GetErpCoreConsultaListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertErpCoresCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteErpCoresCommand { Id = id });

            return NoContent();
        }



        [HttpGet(Name = "GetErpCoreDetalle")]
        
        public async Task<ActionResult<ErpCoreDetalleVm>> GetErpCoreDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetErpCoreDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaERPDependencias(ValidaERPDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



    }
}
