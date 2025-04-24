using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Especialidades.Queries.GetListaEspecialidad;
using NovaExpediente.Application.Especialidades.Queries.GetListaEspecialidadConsulta;
using NovaExpediente.Application.Especialidades.Commands.DeleteEspecialidad;
using NovaExpediente.Application.Especialidades.Commands.UpsertEspecialidad;
using NovaExpediente.Application.Especialidades.Commands.ValidaEspecialidadDependencias;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class EspecialidadController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<EspecialidadListVm>> GetEspecialidad()
        {
            return Ok(await Mediator.Send(new GetEspecialidadListQuery()));
        }

        [HttpGet(Name = "GetEspecialidadConsulta")]
        
        public async Task<ActionResult<EspecialidadConsultaListVm>> GetEspecialidadConsulta()
        {
            return Ok(await Mediator.Send(new GetEspecialidadConsultaListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertEspecialidadCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEspecialidadCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaEspecialidadDependencias(ValidaEspecialidadDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


    }
}
