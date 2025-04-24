using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.AplicacionMovil.Version.Queries.GetVersionActual;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace NovaExpediente.WebUI.Controllers
{
    /// <summary>
    /// Controller for managing catalog value data.
    /// </summary
    [Authorize]
    public class CatalogoValorController : BaseController
    {
        /// <summary>
        /// Obtiene la versión actual de la aplicación.
        /// </summary>
        /// <returns>La versión actual.</returns>
        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<VersionVm>> GetVersionActual()
        {
            try
            {
                var version = await Mediator.Send(new GetVersionActualQuery());
                return Ok(version);
            }
            catch (Exception ex)
            {
                // Loguear error.
                // Retornar como respuesta código 500 Internal Server Error.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
