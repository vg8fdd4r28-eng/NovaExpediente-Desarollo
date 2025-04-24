using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdioma;
using NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaXdioma;
using NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaConsulta;
using NovaExpediente.Application.NivelesIdiomas.Commands.DeleteNivelesIdiomas;
using NovaExpediente.Application.NivelesIdiomas.Commands.UpsertNivelesIdiomas;
using NovaExpediente.Application.NivelesIdiomas.Queries.GetNivelIdiomaDetalle;

using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class NivelIdiomaController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<NivelIdiomaListVm>> GetNivelIdioma()
        {
            return Ok(await Mediator.Send(new GetNivelIdiomaListQuery()));
        }

        [HttpGet (Name = "GetNivelIdiomaConsulta")]
        
        public async Task<ActionResult<NivelIdiomaConsultaListVm>> GetNivelIdiomaConsulta()
        {
            return Ok(await Mediator.Send(new GetNivelIdiomaConsultaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteNivelesIdiomasCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertNivelesIdiomasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



        [HttpGet]
        
        public async Task<ActionResult<NivelIdiomaDetalleVm>> GetDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetNivelIdiomaDetalleListQuery { Id = id }));
        }


        [HttpGet]
        
        public async Task<ActionResult<NivelIdiomaXdiomaListVm>> GetNivelIdiomaXIdioma(int id)
        {
            return Ok(await Mediator.Send(new GetNivelIdiomaXdiomaListQuery { Id = id }));
        }

    }
}
