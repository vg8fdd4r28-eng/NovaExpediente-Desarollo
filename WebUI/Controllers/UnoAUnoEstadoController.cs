using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NovaExpediente.Application.UnosAUnosEstados.Queries.GetListaUnosAUnosEstados;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class UnoAUnoEstadoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ListaUnosAUnosEstadosVm>> GetUnosAUnosEstados()
        {
            return Ok(await Mediator.Send(new GetListaUnosAUnosEstadosQuery()));
        }
    }
}
