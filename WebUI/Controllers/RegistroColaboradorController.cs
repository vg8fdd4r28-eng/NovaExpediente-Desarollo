using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.FuncionesAdministrativas.Commands.RegistroColaborador;
using NovaExpediente.Application.FuncionesAdministrativas.Commands.DatosLaborales;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class RegistroColaboradorController : BaseController
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RegistroColaborador(RegistroColaboradorCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DatosLaborales(DatosLaboralesCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



    }
}
