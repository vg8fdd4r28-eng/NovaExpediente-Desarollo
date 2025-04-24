using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.MasterData.Commands.DeleteSolicitudes;
using NovaExpediente.Application.MasterData.Commands.UpsertSolicitudes;
using NovaExpediente.Application.MasterData.Commands.Workflow;
using NovaExpediente.Application.MasterData.Commands.CambiarResponsable;
using NovaExpediente.Application.MasterData.Commands.AnularSolicitud;
using NovaExpediente.Application.MasterData.Commands.EnviaSolicitudMobile;
using NovaExpediente.Application.MasterData.Queries.GetColaboradoresPorPerfil;
using NovaExpediente.Application.MasterData.Queries.GetDetalleSolicitud;
using NovaExpediente.Application.MasterData.Queries.GetExperienciaActual;
using NovaExpediente.Application.MasterData.Queries.GetListaSolicitudes;
using NovaExpediente.Application.MasterData.Queries.GetSolicitudesPorPerfil;
using NovaExpediente.Application.MasterData.Queries.GetSolicitudPorAprobar;
using NovaExpediente.Application.MasterData.Queries.GetVacacionesPorPerfil;
using NovaExpediente.Application.MasterData.Queries.GetVacacionesAPP;
using NovaExpediente.Application.MasterData.Queries.GetResponsablePorPerfil;
using NovaExpediente.Application.MasterData.Queries.GetColaboradoresAllPorPerfil;


using System.Threading.Tasks;
using System;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class MasterDataController : BaseController
    {
        ICurrentUserService _currentUserService;
        public MasterDataController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        #region Lista de solicitudes
        [HttpGet]

        public async Task<ActionResult<ListaSolicitudesVm>> GetSolicitudes()
        {
            return Ok(await Mediator.Send(new GetListaSolicitudesQuery()));
        }

        [HttpGet(Name = "GetSolicitudesBuzon")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ListaSolicitudesVm>> GetSolicitudesBuzon([FromQuery] GetListaSolicitudesBuzonQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
        #endregion

        #region CRUD
        [HttpPost]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertSolicitudesCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSolicitudesCommand { Id = id });

            return NoContent();
        }
        #endregion

        #region Workflow
        [HttpPost]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EnviarSolicitud(WorkflowCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        #endregion

        #region Detalle de una solicitud
        [HttpGet(Name = "GetDetalleSolicitud")]

        public async Task<ActionResult<DetalleSolicitudVm>> GetDetalleSolicitud(int id)
        {
            return Ok(await Mediator.Send(new GetDetalleSolicitudQuery { Id = id }));
        }
        #endregion

        #region Lista de tipos de solicitud por perfil
        [HttpGet(Name = "GetSolicitudesPorPerfil")]

        public async Task<ActionResult<SolicitudesPorPerfilVm>> GetSolicitudesPorPerfil(string categoria)
        {
            CurrentUserController user = new CurrentUserController(_currentUserService);

            return Ok(await Mediator.Send(new GetSolicitudesPorPerfilQuery { Roles = user.GetUserRoles(), Categoria = categoria }));
        }
        #endregion
        #region Lista de colaboradores según perfil
        [HttpGet(Name = "GetColaboradoresPorPerfil")]

        public async Task<ActionResult<ColaboradoresPorPerfilVm>> GetColaboradoresPorPerfil(int idColaborador)
        {
            CurrentUserController user = new CurrentUserController(_currentUserService);

            return Ok(await Mediator.Send(new GetColaboradoresPorPerfilQuery { IdColaborador = idColaborador, Roles = user.GetUserRoles() }));
        }
        #endregion

        #region Lista de colaboradores según perfil sin filtro
        [HttpGet(Name = "GetColaboradoresAllPorPerfil")]

        public async Task<ActionResult<ColaboradoresAllPorPerfilVm>> GetColaboradoresAllPorPerfil(int idColaborador)
        {
            CurrentUserController user = new CurrentUserController(_currentUserService);

            return Ok(await Mediator.Send(new GetColaboradoresAllPorPerfilQuery { IdColaborador = idColaborador, Roles = user.GetUserRoles() }));
        }
        #endregion

        #region Lista de vacaciones según perfil
        [HttpGet(Name = "GetVacacionesPorPerfil")]

        public async Task<ActionResult<ColaboradoresPorPerfilVm>> GetVacacionesPorPerfil(int idColaborador, DateTime fechaInicio, DateTime fechaFin)
        {
            CurrentUserController user = new CurrentUserController(_currentUserService);

            return Ok(await Mediator.Send(new GetVacacionesPorPerfilQuery
            {
                IdColaborador = idColaborador,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Roles = user.GetUserRoles()
            }));
        }
        #endregion

        #region Lista de responsables según perfil
        [HttpGet(Name = "GetResponsablePorPerfil")]

        public async Task<ActionResult<ResponsablePorPerfilVm>> GetResponsablePorPerfil(int idColaborador, int IdColaboradorSolicitud, int idTipoSolicitud, string estadoSolicitud)
        {


            return Ok(await Mediator.Send(new GetResponsablePorPerfilQuery
            {
                IdColaborador = idColaborador,
                IdColaboradorSolicitud = IdColaboradorSolicitud,
                idTipoSolicitud = idTipoSolicitud,
                estadoSolicitud = estadoSolicitud
            }));
        }
        #endregion

        [HttpPost(Name = "GetExperienciaActual")]

        public async Task<ActionResult<ExperienciaActualVm>> GetExperienciaActual(GetExperienciaActualQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost(Name = "GetSolicitudPorAprobar")]

        public async Task<ActionResult<SolicitudPorAprobarVm>> GetSolicitudPorAprobar(GetSolicitudPorAprobarQuery request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpPost(Name = "CambiarResponsable")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CambiarResponsable(CambiarResponsableCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpPost(Name = "AnularSolicitud")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AnularSolicitud(AnularSolicitudCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EnviaSolicitudMobile(EnviaSolicitudMobileCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        #region Lista de vacaciones APP
        [HttpGet(Name = "GetVacacionesAPP")]
        public async Task<ActionResult<VacacionesAPPVm>> GetVacacionesAPP(int idColaborador)
        {

            return Ok(await Mediator.Send(new GetVacacionesAPPQuery
            {
                IdColaborador = idColaborador
            }));
        }
        #endregion

    }
}
