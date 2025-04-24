using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Colaboradores.Commands.AsociarDMColaborador;
using NovaExpediente.Application.Colaboradores.Commands.AsociarEmpresaColaborador;
using NovaExpediente.Application.Colaboradores.Commands.ReasignarDMPorCliente;
using NovaExpediente.Application.Colaboradores.Commands.ActualizarDatosAPP;
using NovaExpediente.Application.Colaboradores.Queries.GetCertificadoCPIC;
using NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaborador;
using NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaboradorResumen;
using NovaExpediente.Application.Colaboradores.Queries.GetFotoColaborador;
using NovaExpediente.Application.Colaboradores.Queries.GetInfoColaboradorEmail;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDM;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDMAsignar;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradores;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresConsulta;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresCV;
using NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacaciones;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    [SwaggerTag("Colaboradores")]
    [Authorize]
    public class ColaboradorController : BaseController
    {
        #region Obtener datos de colaboradores
        [HttpGet]
        
        public async Task<ActionResult<ColaboradoresListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetColaboradoresListQuery()));
        }


        [HttpGet(Name = "GetDetail")]
        
        public async Task<ActionResult<ColaboradorVm>> GetDetail(int id)
        {
            return Ok(await Mediator.Send(new GetColaboradorListKeyQuery { Id = id }));
        }

        [HttpGet(Name = "GetInfoByEmail")]
        
        public async Task<ActionResult<InfoColaboradorVM>> GetInfoByEmail(string email)
        {
            return Ok(await Mediator.Send(new GetInfoByEmailQuery { Email = email }));
        }

        [HttpGet(Name = "GetDetailResumen")]
        
        public async Task<ActionResult<ColaboradorResumenVm>> GetDetailResumen(int id)
        {
            return Ok(await Mediator.Send(new GetColaboradorResumenListKeyQuery { Id = id }));
        }

        #endregion

        #region Actualizar datos de colaboradores
        [HttpPost(Name = "ActualizaDatosPersonalesAPP")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ActualizaDatosPersonalesAPP(ActualizarDatosAPPCommand command)
        {
            var res = await Mediator.Send(command);
            return Ok(res);
        }
        #endregion

        #region Obtener foto de un colaborador
        [HttpGet(Name = "GetPhotoById")]
        
        public async Task<ActionResult<FotoColaboradorVm>> GetPhotoById(int id)
        {
            return Ok(await Mediator.Send(new GetPhotoByIdQuery { Id = id }));
        }

        [HttpGet(Name = "GetPhotoByEmail")]
        
        public async Task<ActionResult<FotoColaboradorVm>> GetPhotoByEmail(string email)
        {
            return Ok(await Mediator.Send(new GetPhotoByEmailQuery { Email = email }));
        }

        [HttpGet(Name = "GetPhotoByIdentityCard")]
        
        public async Task<ActionResult<FotoColaboradorVm>> GetPhotoByIdentityCard(string identificacion)
        {
            return Ok(await Mediator.Send(new GetPhotoByIdentityCardQuery { Identificacion = identificacion }));
        }
        #endregion

        #region Obtener certificado CPIC
        [HttpGet(Name = "GetCertificadoCPICById")]
        
        public async Task<ActionResult<CertificadoCPICVm>> GetCertificadoCPICById(int id)
        {
            return Ok(await Mediator.Send(new GetCertificadoCPICByIdQuery { Id = id }));
        }
        #endregion

        #region Colaboradores DM
        [HttpGet(Name = "GetColaboradoresDM")]
        
        public async Task<ActionResult<ColaboradorDMListVm>> GetColaboradoresDM()
        {
            return Ok(await Mediator.Send(new GetColaboradorDMListQuery()));
        }

        [HttpGet(Name = "GetColaboradoresDMAsignar")]
        
        public async Task<ActionResult<ColaboradorDMAsignarVm>> GetColaboradoresDMAsignar(int id)
        {
            return Ok(await Mediator.Send(new GetColaboradorDMAsignarListQuery { Id = id }));
        }
        #endregion

        #region Asociar cambios a un colaborador
        [HttpPost(Name = "AsociarDMColaboradorCommand")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AsociarDMColaboradorCommand(AsociarDMColaboradorCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPost(Name = "AsociarEmpresaColaboradorCommand")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AsociarEmpresaColaboradorCommand(AsociarEmpresaColaboradorCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


        [HttpPost(Name = "ReasignarDMPorClienteCommand")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ReasignarDMPorClienteCommand(ReasignarDMPorClienteCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        #endregion

        #region Consulta avanzada
        [HttpGet(Name = "GetColaboradoresConsulta")]
        
        public async Task<ActionResult<ColaboradoresConsultaVm>> GetColaboradoresConsulta(string query)
        {
            return Ok(await Mediator.Send(new GetColaboradoresConsultaListQuery { Query = query }));
        }
        #endregion

        #region Generación de CV
        [HttpGet(Name = "GetColaboradoresCV")]
        
        public async Task<ActionResult<ColaboradoresCVVm>> GetColaboradoresCV(string query, int tipoCV)
        {
            return Ok(await Mediator.Send(new GetColaboradoresCVListQuery { Query = query, TipoCV = tipoCV }));
        }
        #endregion

        #region Vacaciones por Colaborador


        [HttpGet(Name = "GetColaboradorVacaciones")]
        
        public async Task<ActionResult<ColaboradorVacacionesVm>> GetColaboradorVacaciones(int id)
        {
            return Ok(await Mediator.Send(new GetColaboradorVacacionesListKeyQuery { Id = id }));
        }

        #endregion

    }
}
