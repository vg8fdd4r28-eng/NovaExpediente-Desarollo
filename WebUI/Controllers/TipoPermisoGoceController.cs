using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.TiposPermisoGoce.Queries.GetListaTipoPermisoGoce;
using System.Threading.Tasks;




namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class TipoPermisoGoceController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<TipoPermisoGoceListVm>> GetTipoPermisoGoce()
        {
            return Ok(await Mediator.Send(new GetTipoPermisoGoceListQuery()));
        }
    }
}
