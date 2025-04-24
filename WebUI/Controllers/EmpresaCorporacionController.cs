using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.EmpresasCorporacion.Queries.GetListaEmpresasCorporacion;
using NovaExpediente.Application.EmpresasCorporacion.Queries.GetEmpresasCorporacionDetalle;
using NovaExpediente.Application.EmpresasCorporacion.Commands.DeleteEmpresasCorporacion;
using NovaExpediente.Application.EmpresasCorporacion.Commands.UpsertEmpresasCorporacion;
using NovaExpediente.Application.EmpresasCorporacion.Commands.ValidaEmpresaDependencias;
using System.Threading.Tasks;



namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class EmpresaCorporacionController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<EmpresaCorporacionListVm>> GetEmpresaCorporacion()
        {
            return Ok(await Mediator.Send(new GetEmpresaCorporacionListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEmpresasCorporacionCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertEmpresasCorporacionCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



        [HttpGet(Name = "GetEmpresaCorporacionDetalle")]
        
        public async Task<ActionResult<EmpresaCorporacionDetalleVm>> GetEmpresaCorporacionDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetEmpresaCorporacionDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaEmpresaDependencias(ValidaEmpresaDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


    }
}
