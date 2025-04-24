
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.VacunacionColaboradores.Queries.GetListaVacunacionColaborador;
using System.Threading.Tasks;
using NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorDetalle;
using NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorDocumento;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorPerfil;
using NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorReporte;

namespace NovaExpediente.WebUI.Controllers
{
    //[Authorize]
    public class VacunacionColaboradorController : BaseController
    {
        ICurrentUserService _currentUserService;

        public VacunacionColaboradorController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        
        public async Task<ActionResult<VacunacionColaboradorListVm>> GetVacunacionColaboradorList()
        {
            return Ok(await Mediator.Send(new GetVacunacionColaboradorListQuery()));
        }

        [HttpGet(Name = "GetVacunacionColaboradorDetail")]
        
        public async Task<ActionResult<VacunacionColaboradorDetalleVm>> GetVacunacionColaboradorDetail(int idColaborador)
        {
            return Ok(await Mediator.Send(new GetVacunacionColaboradorDetalleListQuery { IdColaborador = idColaborador }));
        }

        [HttpGet(Name = "GetVacunacionColaboradorPerfil")]
        
        public async Task<ActionResult<VacunacionColaboradorPerfilVm>> GetVacunacionColaboradorPerfil(int idColaborador)
        {
            return Ok(await Mediator.Send(new GetVacunacionColaboradorPerfilListQuery { IdColaborador = idColaborador }));
        }

        [HttpGet(Name = "GetVacunacionColaboradorDocumento")]
        
        public async Task<ActionResult<VacunacionColaboradorDetalleVm>> GetVacunacionColaboradorDocumento(int idColaborador)
        {
            return Ok(await Mediator.Send(new GetVacunacionColaboradorDocumentoListQuery { IdColaborador = idColaborador }));
        }

        [HttpGet(Name = "GetVacunacionColaboradorReporte")]
        
        public async Task<ActionResult<VacunacionColaboradorReporteVm>> GetVacunacionColaboradorReporte()
        {
            CurrentUserController user = new CurrentUserController(_currentUserService);
            return Ok(await Mediator.Send(new GetVacunacionColaboradorReporteListQuery { IdColaborador = 0, Roles = user.GetUserRoles() }));
        }
    }
}
