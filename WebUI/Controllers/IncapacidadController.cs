using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.FuncionesAdministrativas.Commands.CargarIncapacidades;
using NovaExpediente.Application.Incapacidades.Queries.GetCargaIncapacidad;
using NovaExpediente.Application.Incapacidades.Queries.GetIncapacidadesPorPerfil;
using System;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{

    [Authorize]
    public class IncapacidadController : BaseController

    {
        ICurrentUserService _currentUserService;
        public IncapacidadController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }


        #region Lista de incapacidades según perfil
        [HttpGet(Name = "GetIncapacidadesPorPerfil")]
        
        public async Task<ActionResult<IncapacidadesPorPerfilVm>> GetIncapacidadesPorPerfil(int idColaborador, DateTime fechaInicio, DateTime fechaFin)
        {
            CurrentUserController user = new CurrentUserController(_currentUserService);

            return Ok(await Mediator.Send(new GetIncapacidadesPorPerfilQuery
            {
                IdColaborador = idColaborador,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Roles = user.GetUserRoles()
            }));
        }
        #endregion

        #region Lista de carga de incapacidades
        [HttpGet(Name = "GetCargaIncapacidad")]
        
        public async Task<ActionResult<CargaIncapacidadListVm>> GetCargaIncapacidad()
        {

            return Ok(await Mediator.Send(new GetCargaIncapacidadListQuery ()));
        }


        #endregion

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CargaIncapacidades([FromForm] IFormFile data, [FromForm] int idColaborador, [FromForm] string Json, [FromForm] int cantidad)
        {


            var id = await Mediator.Send(new CargarIncapacidadesCommand { Json = Json, nombreArchivo = data.FileName, cantRegistros = cantidad, idUsuarioCrea = idColaborador });

            return Ok(id);
        }



    }
}
