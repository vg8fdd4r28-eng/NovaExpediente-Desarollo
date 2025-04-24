using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Seguridad;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [SwaggerTag("CurrentUser")]
    [Authorize]
    public class CurrentUserController : BaseController
    {
        ICurrentUserService _currentUserService;
        public CurrentUserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        [HttpGet(Name = "GetUserRoles")]
        [Authorize]
        public  List<string> GetUserRoles()
        {
            return _currentUserService.GetRoles();
        }

        [HttpGet(Name = "GetUserPermissions")]
        [Authorize]
        public async Task<List<string>> GetUserPermissions()
        {
            return await _currentUserService.GetPermissions();
        }

        [HttpGet(Name = "GetSecurityContext")]
        [Authorize]
        public async Task<ContextoSeguridadVm> GetSecurityContext()
        {
            ContextoSeguridadVm contexto = new ContextoSeguridadVm();
            contexto.Roles = _currentUserService.GetRoles();
            contexto.Permisos =  await _currentUserService.GetPermissions();

            return contexto;
        }
    }
}
