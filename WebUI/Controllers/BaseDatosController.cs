using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.BasesDatos.Queries.GetListaBaseDatos;
using NovaExpediente.Application.BasesDatos.Queries.GetBaseDatosDetalle;
using NovaExpediente.Application.BasesDatos.Queries.GetListaBaseDatosConsulta;
using NovaExpediente.Application.BasesDatos.Commands.DeleteBaseDatos;
using NovaExpediente.Application.BasesDatos.Commands.UpsertBaseDatos;
using NovaExpediente.Application.BasesDatos.Commands.ValidaBaseDatosDependencias;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class BaseDatosController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<BaseDatosListVm>> GetBaseDatos()
        {
            return Ok(await Mediator.Send(new GetBaseDatosListQuery()));
        }

        [HttpGet (Name = "GetBaseDatosConsulta")]
        
        public async Task<ActionResult<BaseDatosConsultaListVm>> GetBaseDatosConsulta()
        {
            return Ok(await Mediator.Send(new GetBaseDatosConsultaListQuery()));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBaseDatosCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertBaseDatosCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet(Name = "GetBaseDatosDetalle")]
        
        public async Task<ActionResult<BaseDatosDetalleVm>> GetBaseDatosDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetBaseDatosDetalleListQuery { Id = id }));
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaBaseDatosDependencias(ValidaBaseDatosDependenciasCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

    }
}
