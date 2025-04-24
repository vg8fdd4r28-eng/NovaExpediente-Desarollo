using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NovaExpediente.Application.Common.Interfaces;
using static NovaExpediente.Application.CurriculumVitae.Commands.CargarCVCommand;

namespace NovaExpediente.Infrastructure.CurriculumVitae
{
    public class CurriculumVitaeService : ICurriculumVitaeService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurriculumVitaeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<(string uuid, string estado, string mensaje)> CargarCVAsync(ModeloCarga document)
        {
            string apiUrl;
            HttpResponseMessage response;
            string estado;
            string mensaje;

            try
            {
                if (document.documentoBase64.Contains(","))
                {
                    document.documentoBase64 = document.documentoBase64.Split(',')[1];
                }

                apiUrl = $"{_configuration["CurriculumVitae:BaseUrlApiCurriculumVitae"]}{_configuration["CurriculumVitae:RutaAPICrearCurriculum"]}";
                var payload = new
                {
                    document = document.documentoBase64,
                    userEmail = document.correo,
                    documentType = document.tipoDocumento,
                    name = document.nombreUsuario
                };
                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(document.uuid))
                    {
                        var uuidResponse = JsonSerializer.Deserialize<Response>(responseContent);
                        document.uuid = uuidResponse.uuid;
                    }

                    estado = "EXITO";
                    mensaje = "Operación exitosa";
                    return (document.uuid, estado, mensaje);
                }
                else
                {
                    estado = "FALLO";
                    mensaje = await response.Content.ReadAsStringAsync();
                    return (document.uuid, estado, mensaje);
                }
            }
            catch (Exception ex)
            {
                estado = "ERROR";
                mensaje = $"Ocurrió un error: {ex.Message}";
                return (document.uuid, estado, mensaje);
            }
        }

        public async Task<(string estado, string mensaje)> ActualizarCVAsync(ModeloCarga document)
        {
            string estado;
            string mensaje;

            try
            {
                if (document.documentoBase64.Contains(","))
                {
                    document.documentoBase64 = document.documentoBase64.Split(',')[1];
                }

                string apiUrl = $"{_configuration["CurriculumVitae:BaseUrlApiCurriculumVitae"]}{string.Format(_configuration["CurriculumVitae:RutaAPIActualizarCurriculum"], document.uuid.ToLower())}";
                var payload = new { document = document.documentoBase64 };
                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    estado = "EXITO";
                    mensaje = "Operación exitosa";
                    return (estado, mensaje);
                }
                else
                {
                    estado = "FALLO";
                    mensaje = await response.Content.ReadAsStringAsync();
                    return (estado, mensaje);
                }
            }
            catch (Exception ex)
            {
                estado = "ERROR";
                mensaje = $"Ocurrió un error: {ex.Message}";
                return (estado, mensaje);
            }
        }


        public class Response
        {
            public string uuid { get; set; }
        }
    }
}
