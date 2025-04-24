using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Permisos.Queries.GetPermisosPorPerfil;
using System;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{

    [Authorize]
    public class PermisoController : BaseController

    {
        ICurrentUserService _currentUserService;
        public PermisoController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }


        #region Lista de Permisos según perfil
        [HttpGet(Name = "GetPermisosPorPerfil")]
        
        public async Task<ActionResult<PermisoPorPerfilVm>> GetPermisosPorPerfil(int idColaborador, DateTime fechaInicio, DateTime fechaFin)
        {

            CurrentUserController user = new CurrentUserController(_currentUserService);

            return Ok(await Mediator.Send(new GetPermisosPorPerfilQuery
            {
                IdColaborador = idColaborador,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Roles = user.GetUserRoles()
            }));
        }
        #endregion







    }
}
