using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.EspecialidadesConsultor.Queries.GetListaEspecialidadConsultor;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class EspecialidadConsultorController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<EspecialidadConsultorListVm>> GetEspecialidadesConsultor(int id)
        {
            return Ok(await Mediator.Send(new GetEspecialidadConsultorListQuery { Id = id }));
        }

    }
}
