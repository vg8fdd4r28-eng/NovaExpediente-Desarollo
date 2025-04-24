using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresCV;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Net = System.Net;
using Microsoft.Extensions.Configuration;

namespace NovaExpediente.Infrastructure.Curriculum
{
   // public class CurriculumService2 : ICurriculumService
    public class CurriculumService2 
    {
        private readonly IConfiguration Configuration;

        public CurriculumService2(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<string> GenerarCV(string JsonCV, int TipoCV)
        {
            string result = "";


            string baseAddress = Configuration.GetSection("GeneracionCV").GetSection("BaseUrlApiCV").Value;
            string rutaAPI = Configuration.GetSection("GeneracionCV").GetSection("RutaAPICV").Value;

           
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var credential = new DataJsonCVVM { json = JsonCV, tipoCV = TipoCV };
            var credentialString = Newtonsoft.Json.JsonConvert.SerializeObject(credential, Newtonsoft.Json.Formatting.None);
            var content = new StringContent(credentialString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(rutaAPI, content);
            if (response.IsSuccessStatusCode)
            { result = await response.Content.ReadAsStringAsync(); }
            else
            {
                result = "Se produjo un error en la generación de Curriculums.";
            }





            return result;
        }


    }
}
