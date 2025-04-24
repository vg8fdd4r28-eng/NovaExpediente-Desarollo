using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Net = System.Net;

namespace NovaExpediente.Infrastructure.Informes
{
    public class InformeService : IInformeService
    {
        private readonly IConfiguration _configuration;

        public InformeService(IConfiguration configuration) => _configuration = configuration;

        private HttpClient InitHttpClient()
        {
            var baseAddress = _configuration
                ?.GetSection("InformesIA")
                ?.GetSection("BaseUrlApiConversacion")
                ?.Value;

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, certChain, policyErrors) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            if (baseAddress != null) client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            return client;
        }

        public async Task<string> ConsultaConversaciones(string userId, int currentPage)
        {
            var result = "";
            try
            {
                var rutaApi = _configuration
                    ?.GetSection("InformesIA")
                    ?.GetSection("RutaAPIConsultaConversaciones")
                    ?.Value;
                if (_configuration != null)
                {
                    var pageSize = _configuration.GetSection("InformesIA").GetValue<int>("pageSize");
                    var formattedRutaApi = string.Format(rutaApi ?? throw new InvalidOperationException(), userId, currentPage, pageSize);

                    var client = InitHttpClient();
                    var response = await client.GetAsync(formattedRutaApi);

                    result = response.IsSuccessStatusCode
                        ? await response.Content.ReadAsStringAsync()
                        : await HandleErrorResponse(response);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public async Task<string> ConsultaConversacionDetalle(string conversationId)
        {
            var result = "";
            try
            {
                var rutaApi = _configuration
                    ?.GetSection("InformesIA")
                    ?.GetSection("RutaAPIConsultaDetalleConversacion")
                    ?.Value;
                
                var formattedRutaApi = string.Format(rutaApi ?? throw new InvalidOperationException(), conversationId);

                var client = InitHttpClient();
                var response = await client.GetAsync(formattedRutaApi);

                result = response.IsSuccessStatusCode
                    ? await response.Content.ReadAsStringAsync()
                    : await HandleErrorResponse(response);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public async Task<string> CrearConversacion(string userId)
        {
            var result = "";
            try
            {
                var rutaApi = _configuration
                    ?.GetSection("InformesIA")
                    ?.GetSection("RutaAPICreaConversaciones")
                    ?.Value;

                var client = InitHttpClient();
                var conversacionRequest = new ConversacionRequest { userId = userId };
                var jsonContent = JsonConvert.SerializeObject(conversacionRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(rutaApi, content);
                result = response.IsSuccessStatusCode
                    ? await response.Content.ReadAsStringAsync()
                    : await HandleErrorResponse(response);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public async Task<string> CrearMensaje(string userId, string conversationId, string message)
        {
            var result = "";
            try
            {
                var rutaApi = _configuration
                    ?.GetSection("InformesIA")
                    ?.GetSection("RutaAPICreaMensaje")
                    ?.Value;
                
                var formattedRutaApi = string.Format(rutaApi ?? throw new InvalidOperationException(), conversationId);

                var client = InitHttpClient();
                var mensajeRequest = new MensajeRequest { userId = userId, message = message };
                var jsonContent = JsonConvert.SerializeObject(mensajeRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(formattedRutaApi, content);
                result = response.IsSuccessStatusCode
                    ? await response.Content.ReadAsStringAsync()
                    : await HandleErrorResponse(response);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        private static async Task<string> HandleErrorResponse(HttpResponseMessage response)
        {
            var errorMessage = "An error occurred while processing the request.";
            try
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(errorContent))
                {
                    var errorObject = JsonConvert.DeserializeObject<dynamic>(errorContent);
                    errorMessage = errorObject?.message ?? errorMessage;
                }
            }
            catch
            {
                // Log error or handle parsing exception
            }

            return $"Error: {errorMessage}";
        }

        private class ConversacionRequest
        {
            public string userId { get; set; }
        }

        private class MensajeRequest
        {
            public string userId { get; set; }
            public string message { get; set; }
        }
    }
}
