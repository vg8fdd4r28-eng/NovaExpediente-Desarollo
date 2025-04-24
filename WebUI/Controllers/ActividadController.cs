using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaExpediente.Application.Actividades.Queries.GetActividadesDiaConsulta;
using NovaExpediente.Application.Actividades.Queries.GetActividadesMesConsulta;
using NovaExpediente.Application.AplicacionMovil.Actividades.Queries.GetListaActividades;
using NovaExpediente.Application.AplicacionMovil.Actividades.Queries.GetDetalleActividad;
using NovaExpediente.Application.AplicacionMovil.Actividades.Commands.DeleteActividades;
using NovaExpediente.Application.AplicacionMovil.Actividades.Commands.DeleteImagesActividades;
using NovaExpediente.Application.AplicacionMovil.Actividades.Commands.UpsertActividades;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NovaExpediente.WebUI.Controllers
{
    [Authorize]
    public class ActividadController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetActividadesMesAPP(int mes, int anho, int IdIdioma)
        {
            return Ok(await Mediator.Send(new GetActividadesMesAPPQuery { Mes = mes, Anho = anho, IdIdioma = IdIdioma }));
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetActividadesDiaAPP(DateTime fecha, int IdIdioma)
        {
            return Ok(await Mediator.Send(new GetActividadesDiaAPPQuery { Fecha = fecha, IdIdioma = IdIdioma }));
        }


        [HttpGet]
        public async Task<ActionResult<ListaActividadesVm>> GetListaActividades()
        {
            return Ok(await Mediator.Send(new GetListaActividadesQuery()));
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetDetalleActividad(int IdActividad)
        {
            return Ok(await Mediator.Send(new GetDetalleActividadQuery { IdActividad = IdActividad }));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActividad(int id)
        {
            var res = await Mediator.Send(new DeleteActividadesCommand { Id = id });
            return Ok(res);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteImagesActividades(int IdActividad, int IdImagen)
        {
            var res = await Mediator.Send(new DeleteImagesActividadesCommand { IdActividad = IdActividad, IdImagen = IdImagen });
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertActividades(UpsertActividadesCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }


    }
}