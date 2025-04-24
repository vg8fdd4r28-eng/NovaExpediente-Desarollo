using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Cursos.Queries.GetListaCursos;
using NovaExpediente.Application.Cursos.Queries.GetCursoDetalle;
using NovaExpediente.Application.Cursos.Commands.DeleteCursos;
using NovaExpediente.Application.Cursos.Commands.UpsertCursos;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CursoController : BaseController
    {

        [HttpGet]
        
        public async Task<ActionResult<CursosListVm>> GetCursos()
        {       
            return Ok(await Mediator.Send(new GetCursosListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCursosComand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertCursosCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpGet]
        
        public async Task<ActionResult<CursoDetalleVm>> GetCursoDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetCursoDetalleListQuery { Id = id }));
        }

    }
}
