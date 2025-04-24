using MediatR;
using Microsoft.AspNetCore.Http;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CloudImages.Commands
{
    public class UploadImageCommand : IRequest<string>
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public string Folder { get; set; }
        public int Id { get; set; }
        public int IdUsuarioCrea { get; set; }
        public class UploadImageCommandCommandHandler : IRequestHandler<UploadImageCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly ICloudinaryService _service;
            public UploadImageCommandCommandHandler(INovaExpedienteDbContext context, ICloudinaryService service)
            {
                _context = context;
                _service = service;
            }

            public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                switch (request.Folder.ToUpper())
                {
                    case "TEST":
                        res = await _service.UploadImage(request.Name, request.File, request.Folder);
                        break;
                    case "CONVENIOS":
                        res = await _service.UploadImageConvenios(request.Name, request.File, request.Folder, request.Id);
                        await _context.CargaImagenConvenios(res, request.Id, request.IdUsuarioCrea);
                        break;
                    case "NOTICIAS":
                        res = await _service.UploadImageNoticias(request.Name, request.File, request.Folder, request.Id);
                        await _context.CargaImagenNoticias(res, request.Id, request.IdUsuarioCrea);
                        break;
                    case "ACTIVIDADES":
                        res = await _service.UploadImageActividades(request.Name, request.File, request.Folder, request.Id);
                        await _context.CargaImagenActividades(res, request.Id, request.IdUsuarioCrea);
                        break;
                    case "SORTEOS":
                        res = await _service.UploadImageSorteo(request.Name, request.File, request.Folder, request.Id);
                        await _context.CargaImagenSorteos(res, request.Id, request.IdUsuarioCrea);
                        break;
                    default:
                        break;
                }



                return res;

            }
        }


    }
}
