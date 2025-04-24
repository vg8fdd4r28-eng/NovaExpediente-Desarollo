using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NovaExpediente.Application.Common.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace NovaExpediente.Infrastructure.CloudService
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration Configuration;
        private Account account;
        private Cloudinary cloudinary;
        private string Cloud_Name;
        private string API_Key;
        private string API_Secret;

        public CloudinaryService(IConfiguration configuration)
        {
            Configuration = configuration;
            Cloud_Name = Configuration.GetSection("Cloudinary").GetSection("CloudName").Value;
            API_Key = Configuration.GetSection("Cloudinary").GetSection("APIKey").Value;
            API_Secret = Configuration.GetSection("Cloudinary").GetSection("APISecret").Value;

            account = new Account(
                      Cloud_Name,// "my_cloud_name"
                       API_Key,//"my_api_key"
                       API_Secret// "my_api_secret"
                                   );



            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
        }

        // public async Task<ImageUploadResult> UploadImage(ImageUploadParams parameters)
        public async Task<string> UploadImage(string name, IFormFile file, string folder)
        {

            string res = "";

            Stream fileStream = file.OpenReadStream();


            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, fileStream),
                Folder= folder,
                Overwrite=true
                //  ,PublicId = name

            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            // var uremoveResult = cloudinary.DeleteDerivedResourcesAsync("");
            res = uploadResult.Url.ToString();

            return res;


        }

        public async Task<string> UploadImageConvenios(string name, IFormFile file, string folder, int Id)
        {

            string res = "";

            Stream fileStream = file.OpenReadStream();


            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, fileStream),
                Folder = folder,
                Overwrite = true
                //  ,PublicId = name

            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            // var uremoveResult = cloudinary.DeleteDerivedResourcesAsync("");
            res = uploadResult.Url.ToString();

   
            return res;


        }

        public async Task<string> UploadImageNoticias(string name, IFormFile file, string folder, int Id)
        {

            string res = "";

            Stream fileStream = file.OpenReadStream();


            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, fileStream),
                Folder = folder,
                Overwrite = true
                //  ,PublicId = name

            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            // var uremoveResult = cloudinary.DeleteDerivedResourcesAsync("");
            res = uploadResult.Url.ToString();

            return res;


        }

        public async Task<string> UploadImageActividades(string name, IFormFile file, string folder, int Id)
        {

            string res = "";

            Stream fileStream = file.OpenReadStream();


            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, fileStream),
                Folder = folder,
                Overwrite = true
                //  ,PublicId = name

            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            // var uremoveResult = cloudinary.DeleteDerivedResourcesAsync("");
            res = uploadResult.Url.ToString();


            return res;


        }

        public async Task<string> UploadImageSorteo(string name, IFormFile file, string folder, int Id)
        {
            string res = "";
            Stream fileStream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, fileStream),
                Folder = folder,
                Overwrite = true
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            res = uploadResult.Url.ToString();
            return res;
        }


    }
}
