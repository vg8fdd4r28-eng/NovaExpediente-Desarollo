using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.GradosAcademicos.Commands.DeleteGradoAcademico;
using NovaExpediente.Application.GradosAcademicos.Commands.UpsertGradoAcademico;
using NovaExpediente.Application.GradosAcademicos.Queries.GetListaGradosAcademicos;
using NovaExpediente.Application.GradosAcademicos.Queries.GetListaGradosAcademicosConsulta;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class GradosAcademicosController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<GradosAcademicosListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetGradosAcademicosListQuery()));
        }


        [HttpGet (Name = "GetAllConsulta")]
        
        public async Task<ActionResult<GradosAcademicosConsultaListVm>> GetAllConsulta()
        {
            return Ok(await Mediator.Send(new GetGradosAcademicosConsultaListQuery()));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertGradoAcademicoCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteGradoAcademicoCommand { Id = id });

            return NoContent();
        }
    }
}
