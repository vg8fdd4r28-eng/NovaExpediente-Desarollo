using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.AmbientesDesarrollo.Commands.DeleteAmbientesDesarrollo;
using NovaExpediente.Application.AmbientesDesarrollo.Commands.UpsertAmbientesDesarrollo;
using NovaExpediente.Application.AmbientesDesarrollo.Commands.ValidaAmbienteDependencias;
using NovaExpediente.Application.AmbientesDesarrollo.Queries.GetAmbienteDesarrolloDetalle;
using NovaExpediente.Application.AmbientesDesarrollo.Queries.GetListaAmbienteDesarrollo;
using NovaExpediente.Application.AmbientesDesarrollo.Queries.GetListaAmbienteDesarrolloConsulta;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class AmbienteDesarrolloController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<AmbienteDesarrolloListVm>> GetAmbienteDesarrollo()
        {
            return Ok(await Mediator.Send(new GetAmbienteDesarrolloListQuery()));
        }

        [HttpGet (Name = "GetAmbienteDesarrolloConsulta")]
        
        public async Task<ActionResult<AmbienteDesarrolloConsultaListVm>> GetAmbienteDesarrolloConsulta()
        {
            return Ok(await Mediator.Send(new GetAmbienteDesarrolloConsultaListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertAmbientesDesarrolloCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteAmbientesDesarrolloCommand { Id = id });

            return NoContent();
        }

        [HttpGet(Name = "GetAmbienteDesarrolloDetalle")]
        
        public async Task<ActionResult<AmbienteDesarrolloDetalleVm>> GetAmbienteDesarrolloDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetAmbienteDesarrolloDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaAmbienteDependencias(ValidaAmbienteDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



    }
}
