using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesPorPerfil;
using NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesConsulta;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Vacaciones.Commands.ActualizaSaldoVacaciones;
using NovaExpediente.Application.Vacaciones.Commands.EnviaSolicitudVacaciones;
using NovaExpediente.Application.Vacaciones.Queries.CalculaCantidadDias;
using NovaExpediente.Application.Vacaciones.Queries.GetCargaVacaciones;
using NovaExpediente.Application.FuncionesAdministrativas.Commands.CargarVacaciones;
using System;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class VacacionesController : BaseController
    {

        ICurrentUserService _currentUserService;
        public VacacionesController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        
        public async Task<ActionResult<ColaboradorVacacionesPorPerfilVm>> GetColaboradorVacacionesPorPerfil(int id)
        {
            CurrentUserController user = new CurrentUserController(_currentUserService);
            return Ok(await Mediator.Send(new GetColaboradorVacacionesPorPerfilListQuery { IdColaborador = id, Roles = user.GetUserRoles() }));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ActualizaSaldoVacaciones(ActualizaSaldoVacacionesCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }



        [HttpGet]
        
        public async Task<ActionResult<CalculaCantidadDiasVm>> GetCalculaCantidadDiasListQuery(int idColaborador, DateTime FechaInicio, DateTime FechaFin)
        {
         return Ok(await Mediator.Send(new GetCalculaCantidadDiasListQuery { IdColaborador = idColaborador, FechaInicio = FechaInicio, FechaFin= FechaFin }));
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EnviaSolicitudVacaciones(EnviaSolicitudVacacionesCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        #region Carga de vacaciones
        [HttpGet(Name = "GetCargaVacaciones")]

        public async Task<ActionResult<CargaVacacionesListVm>> GetCargaVacaciones()
        {

            return Ok(await Mediator.Send(new GetCargaVacacionesListQuery()));
        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CargaVacaciones([FromForm] IFormFile data, [FromForm] int idColaborador, [FromForm] string Json, [FromForm] int cantidad)
        {


            var id = await Mediator.Send(new CargarVacacionesCommand { Json = Json, nombreArchivo = data.FileName, cantRegistros = cantidad, idUsuarioCrea = idColaborador });

            return Ok(id);
        }



        #endregion

        [HttpGet]

        public async Task<ActionResult<ColaboradorVacacionesConsultaVm>> GetColaboradorVacacionesConsulta(int id)
        {
            CurrentUserController user = new CurrentUserController(_currentUserService);
            return Ok(await Mediator.Send(new GetColaboradorVacacionesConsultaListQuery { IdColaborador = id, Roles = user.GetUserRoles() }));
        }

    }
}
