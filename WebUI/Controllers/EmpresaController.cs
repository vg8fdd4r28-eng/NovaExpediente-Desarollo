using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Empresas.Queries.GetListaEmpresas;
using NovaExpediente.Application.Empresas.Queries.GetEmpresaDetalle;
using NovaExpediente.Application.Empresas.Commands.DeleteEmpresas;
using NovaExpediente.Application.Empresas.Commands.UpsertEmpresas;
using System.Threading.Tasks;



namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class EmpresController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<EmpresaListVm>> GetEmpresas()
        {
            return Ok(await Mediator.Send(new GetEmpresaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEmpresaCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertEmpresasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



        [HttpGet(Name = "GetEmpresaDetalle")]
        
        public async Task<ActionResult<EmpresaDetalleVm>> GetEmpresaDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetEmpresaDetalleListQuery { Id = id }));
        }


    }
}
