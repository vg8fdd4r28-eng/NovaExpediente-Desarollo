using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Tecnologias.Queries.GetListaTecnologia;
using NovaExpediente.Application.Tecnologias.Queries.GetTecnologiaDetalle;
using NovaExpediente.Application.Tecnologias.Queries.GetListaTecnologiaConsulta;
using NovaExpediente.Application.Tecnologias.Commands.UpsertTecnologia;
using NovaExpediente.Application.Tecnologias.Commands.DeleteTecnologia;
using NovaExpediente.Application.Tecnologias.Commands.ValidaTecnologiaDependencias;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class TecnologiaController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<TecnologiaListVm>> GetTecnologia()
        {
            return Ok(await Mediator.Send(new GetTecnologiaListQuery()));
        }

        [HttpGet(Name = "GetTecnologiaConsulta")]
        
        public async Task<ActionResult<TecnologiaConsultaListVm>> GetTecnologiaConsulta()
        {
            return Ok(await Mediator.Send(new GetTecnologiaConsultaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTecnologiaCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertTecnologiaCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet]
        
        public async Task<ActionResult<TecnologiaDetalleVm>> GetTecnologiaDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetTecnologiaDetalleListQuery { Id = id }));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaTecnologiaDependencias(ValidaTecnologiaDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
