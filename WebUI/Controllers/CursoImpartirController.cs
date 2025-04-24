using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.CursosImpartir.Queries.GetListaCursosImpartir;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class CursoImpartirController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<CursoImpartirListVm>> GetCursosImpartir(int id)
        {
            return Ok(await Mediator.Send(new GetCursoImpartirListQuery { Id = id }));
        }

    }
}
