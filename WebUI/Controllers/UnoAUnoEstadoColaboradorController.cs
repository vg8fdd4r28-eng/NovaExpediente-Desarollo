using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.UnoAUnoEstadosColaboradores.Queries.GetListaUnoAUnoEstadosColaboradores;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class UnoAUnoEstadoColaboradorController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ListaUnosAUnosEstadosColaboradoresVm>> GetUnosAUnosEstadosColaboradores()
        {
            return Ok(await Mediator.Send(new GetListaUnosAUnosEstadosColaboradoresQuery() ));
        }
    }
}
