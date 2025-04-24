using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaExperienciaProyecto;
using NovaExpediente.Application.ExperienciasProyectos.Queries.GetExperienciaProyectoDetalle;
using NovaExpediente.Application.ExperienciasProyectos.Commands.DeleteExperienciasProyectos;
using NovaExpediente.Application.ExperienciasProyectos.Commands.UpsertExperienciasProyectos;
using System.Threading.Tasks;
using NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaHerramientas;
using NovaExpediente.Application.ExperienciasProyectos.Queries.GetProyectosCliente;
using System;
using NovaExpediente.Application.ExperienciasProyectos.Queries.GetProyectoClienteValido;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaProyectoController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ExperienciaProyectoListVm>> GetExperienciaProyecto(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaProyectoListQuery { Id = id }));
        }


        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteExperienciaProyectoCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertExperienciaProyectoCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpGet]
        
        public async Task<ActionResult<ExperienciaProyectoDetalleVm>> GetExperienciaProyectoDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetExperienciaProyectoDetalleListQuery { Id = id }));
        }

        [HttpGet]
        
        public async Task<ActionResult<ListaHerramientasVm>> GetListaHerramientas()
        {
            return Ok(await Mediator.Send(new GetListaHerramientasQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<ListaProyectosClienteVm>> GetProyectosCliente(string nombreCliente, int? idCliente)
        {
            return Ok(await Mediator.Send(new GetProyectosClienteQuery() { NombreCliente = nombreCliente, IdCliente = idCliente }));
        }

        [HttpPost]
        
        public async Task<ActionResult<ProyectoClienteValidoVm>> ValidarProyecto(GetProyectoClienteValidoQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}