using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Certificaciones.Queries.GetCantidadCertificaciones;
using NovaExpediente.Application.Clientes.Queries.GetCantidadClientes;
using NovaExpediente.Application.Colaboradores.Queries.GetCantidadColaboradoresDM;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresEspecialidad;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresGenero;
using NovaExpediente.Application.ExperienciasBaseDatos.Queries.GetCantidadColabradoresPorBaseDatos;
using NovaExpediente.Application.MasterData.Queries.GetAntiguedadExpediente;
using NovaExpediente.Application.MasterData.Queries.GetAntiguedadSolicitudes;
using NovaExpediente.Application.MasterData.Queries.GetCantidadSolicitudes;
using NovaExpediente.Application.MasterData.Queries.GetSolicitudesEnTransito;
using NovaExpediente.Application.MasterData.Queries.GetUsoAplicacion;
using System.Threading.Tasks;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresAsignadosDM;


namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        [HttpGet]
        
        public async Task<ActionResult<ColaboradoresGeneroListVm>> GetColaboradoresPorGenero()
        {
            return Ok(await Mediator.Send(new GetColaboradoresGeneroListQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<CantidadCertificacionesListVm>> GetColaboradoresPorTipoDeCertificado()
        {
            return Ok(await Mediator.Send(new GetCantidadCertificacionesListQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<ColaboradoresEspecialidadListVm>> GetColaboradoresPorEspecialidad()
        {
            return Ok(await Mediator.Send(new GetColaboradoresEspecialidadListQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<ColaboradoresBaseDatosListVm>> GetColaboradoresPorBaseDeDatos()
        {
            return Ok(await Mediator.Send(new GetColaboradoresBaseDatosListQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<CantidadColaboradoresDMListVm>> GetColaboradoresPorDM()
        {
            return Ok(await Mediator.Send(new GetCantidadColaboradoresDMListQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<CantidadSolicitudesVm>> GetSolicitudes()
        {
            return Ok(await Mediator.Send(new GetCantidadSolicitudesQuery()));
        }


        [HttpGet]
        
        public async Task<ActionResult<CantidadClientesVm>> GetClientes()
        {
            return Ok(await Mediator.Send(new GetCantidadClientesQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<SolicitudesEnTransitoVm>> GetSolicitudesEnTransito()
        {
            return Ok(await Mediator.Send(new GetSolicitudesEnTransitoQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<CantidadPorAntiguedadVm>> GetAntiguedadSolicitudes()
        {
            return Ok(await Mediator.Send(new GetAntiguedadSolicitudesQuery()));
        }

        [HttpGet]
        
        public async Task<ActionResult<CantidadPorAntiguedadVm>> GetAntiguedadExpediente()
        {
            return Ok(await Mediator.Send(new GetAntiguedadExpedienteQuery()));
        }
        [HttpPost]
        
        public async Task<ActionResult<UsoAplicacionVm>> GetUsoAplicacion(GetUsoAplicacionQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        
        public async Task<ActionResult<ColaboradoresAsignadosDMListVm>> GetColaboradoresAsignados(int id)
        {
            return Ok(await Mediator.Send(new GetColaboradoresAsignadosListQuery() { IdDM = id }));
        }
    }
}
