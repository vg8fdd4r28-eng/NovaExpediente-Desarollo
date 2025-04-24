using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NovaExpediente.Application.CloudImages.Commands;

namespace NovaExpediente.WebUI.Controllers
{

    [Authorize]
    public class CloudImagesController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile data, [FromForm] string name)
        {
            var id = await Mediator.Send(new UploadImageCommand { File = data, Name = data.FileName, Folder = "TEST", Id = 0, IdUsuarioCrea = 0 });

            return Ok(id);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UploadImageConvenios([FromForm] IFormFile data, [FromForm] string name, [FromForm] int IdConvenio, [FromForm] int IdUsuarioCrea)
        {
            var id = await Mediator.Send(new UploadImageCommand { File = data, Name = data.FileName, Folder = "CONVENIOS", Id = IdConvenio, IdUsuarioCrea = IdUsuarioCrea });

            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UploadImageNoticias([FromForm] IFormFile data, [FromForm] string name, [FromForm] int IdNoticia, [FromForm] int IdUsuarioCrea)
        {
            var id = await Mediator.Send(new UploadImageCommand { File = data, Name = data.FileName, Folder = "NOTICIAS", Id = IdNoticia, IdUsuarioCrea = IdUsuarioCrea });

            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UploadImageActividades([FromForm] IFormFile data, [FromForm] string name, [FromForm] int IdActividad, [FromForm] int IdUsuarioCrea)
        {
            var id = await Mediator.Send(new UploadImageCommand { File = data, Name = data.FileName, Folder = "ACTIVIDADES", Id = IdActividad, IdUsuarioCrea = IdUsuarioCrea });

            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UploadImageSorteo([FromForm] IFormFile data, [FromForm] string name, [FromForm] int IdSorteo, [FromForm] int IdUsuarioCrea)
        {
            var id = await Mediator.Send(new UploadImageCommand { File = data, Name = data.FileName, Folder = "SORTEOS", Id = IdSorteo, IdUsuarioCrea = IdUsuarioCrea });
            return Ok(id);
        }
    }
}
