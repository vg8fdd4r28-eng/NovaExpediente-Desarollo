using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.ColaboradorProyectos.Queries.GetListaColaboradorProyectos;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [SwaggerTag("Colaborador - Proyectos")]

    [Authorize]
    public class ColaboradorProyectoController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ColaboradorProyectoListVm>> GetColaboradorProyectos(int id)
        {
            return Ok(await Mediator.Send(new GetColaboradorProyectoListQuery { Id = id }));
        }

    }
}