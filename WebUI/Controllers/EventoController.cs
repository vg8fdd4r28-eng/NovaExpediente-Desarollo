using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NovaExpediente.Application.AplicacionMovil.Eventos.GetListaEventos;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
        public class EventoController : BaseController
        {
            [HttpGet]
            public async Task<ActionResult<JObject>> GetListaEventos()
            {
                return Ok(await Mediator.Send(new GetListaEventosQuery()));
            }
        }
    }
