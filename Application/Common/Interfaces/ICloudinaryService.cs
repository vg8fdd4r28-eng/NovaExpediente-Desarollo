using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface ICloudinaryService
    {

        Task<string> UploadImage(string name, IFormFile file, string folder);
        Task<string> UploadImageNoticias(string name, IFormFile file, string folder, int Id);
        Task<string> UploadImageConvenios(string name, IFormFile file, string folder, int Id);
        Task<string> UploadImageActividades(string name, IFormFile file, string folder, int Id);
        Task<string> UploadImageSorteo(string name, IFormFile file, string folder, int Id);

    }
}
