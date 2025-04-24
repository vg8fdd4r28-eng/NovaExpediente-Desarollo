using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasBaseDatos.Queries.GetListaExperienciasBaseDatos;
using System.Threading.Tasks;
namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaBaseDatosController  : BaseController
    {
        [HttpGet]
    
    public async Task<ActionResult<ExperienciaBaseDatosListVm>> GetExperienciasBaseDatos(int id)
    {
        return Ok(await Mediator.Send(new GetExperienciaBaseDatosListQuery { Id = id }));
    }

}
}