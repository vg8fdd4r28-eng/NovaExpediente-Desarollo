using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.UnosAUnosCategorias.Queries.GetListaUnosAUnosCategorias;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class UnoAUnoCategoriaController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ListaUnosAUnosCategoriasVm>> GetUnosAUnosCategorias()
        {
            return Ok(await Mediator.Send(new GetListaUnosAUnosCategoriasQuery()));
        }
    }
}
