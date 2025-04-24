using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ExperienciasAmbientesDesarollo.Queries.GetListaExperienciasAmbientesDesarrollo;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ExperienciaAmbienteDesarrolloController : BaseController
    {
        [HttpGet]
    
    public async Task<ActionResult<ExperienciaAmbienteDesarrolloListVm>> GetExperienciaAmbienteDesarrollo(int id)
    {
        return Ok(await Mediator.Send(new GetExperienciaAmbienteDesarrolloListQuery { Id = id }));
    }

}
}