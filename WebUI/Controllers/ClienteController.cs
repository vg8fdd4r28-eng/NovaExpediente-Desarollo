using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Clientes.Queries.GetListaCliente;
using NovaExpediente.Application.Clientes.Queries.GetListaClienteReferencias;
using NovaExpediente.Application.Clientes.Commands.DeleteClientes;
using NovaExpediente.Application.Clientes.Commands.UpsertClientes;
using NovaExpediente.Application.Clientes.Commands.ValidaClienteDependencias;
using NovaExpediente.Application.Clientes.Queries.GetClienteDetalle;
using System.Threading.Tasks;
using NovaExpediente.Application.Clientes.Queries.GetClientesEmpresa;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ClienteController : BaseController
    {
        [HttpGet(Name = "GetClientes")]
        
        public async Task<ActionResult<ClienteListVm>> GetClientes()
        {
            return Ok(await Mediator.Send(new GetClienteListQuery()));
        }

        [HttpGet(Name = "GetClientesReferencias")]
        
        public async Task<ActionResult<ClienteReferenciasListVm>> GetClientesReferencias()
        {
            return Ok(await Mediator.Send(new GetClienteReferenciasListQuery()));
        }

        [HttpGet(Name = "GetClientesEmpresa")]
        
        public async Task<ActionResult<ClientesEmpresaListVm>> GetClientesEmpresa(int id)
        {
            return Ok(await Mediator.Send(new GetClientesEmpresaListQuery() { Id = id}));
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteClienteCommand { Id = id });

            return NoContent();
        }

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertClienteCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpGet(Name = "GetClienteDetalle")]
        
        public async Task<ActionResult<ClienteDetalleVm>> GetClienteDetalle(int id)
        {
            return Ok(await Mediator.Send(new GetClienteDetalleListQuery { Id = id }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidaClienteDependencias(ValidaClienteDependenciasCommand command)
        {
            var res = await Mediator.Send(command);

            return Ok(res);
        }





    }
}
