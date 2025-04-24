using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Net = System.Net;

namespace NovaExpediente.Infrastructure.Conversacion
{
    public class ConversacionService : IConversacionService
    {
        private readonly IConfiguration _configuration;

        public ConversacionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private HttpClient InitHttpClient()
        {
            string baseAddress = _configuration.GetSection("Conversacion").GetSection("BaseUrlApiConversacion").Value;

            var handler = new HttpClientHandler();
            
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, certChain, policyErrors) =>
                {
                    return true; 
                };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async Task<string> ConsultaConversaciones(string userId,int currentPage)
        {
            string result = "";
            try
            {

                string rutaAPI = _configuration.GetSection("Conversacion").GetSection("RutaAPIConsultaConversaciones").Value;
                int pageSize = _configuration.GetSection("Conversacion").GetValue<int>("pageSize");
                string formattedRutaAPI = string.Format(rutaAPI, userId, currentPage, pageSize);

                var client = InitHttpClient();

                var response = await client.GetAsync(formattedRutaAPI);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    result = await HandleErrorResponse(response);
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
            string result = "";
            try
            {

                string rutaAPI = _configuration.GetSection("Conversacion").GetSection("RutaAPIConsultaDetalleConversacion").Value;
                string formattedRutaAPI = string.Format(rutaAPI, conversationId);

                var client = InitHttpClient();

                var response = await client.GetAsync(formattedRutaAPI);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    result = await HandleErrorResponse(response);
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public async Task<string> CrearConversacion(string userId)
        {
            string result = "";
            try
            {

                string rutaAPI = _configuration.GetSection("Conversacion").GetSection("RutaAPICreaConversaciones").Value;

                var client = InitHttpClient();

                // Serializar el objeto a JSON
                var ConversacionRequest = new ConversacionRequest{userId = userId};
                var jsonContent = JsonConvert.SerializeObject(ConversacionRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(rutaAPI, content);

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
            string result = "";
            try
            {

                string rutaAPI = _configuration.GetSection("Conversacion").GetSection("RutaAPICreaMensaje").Value;
                string formattedRutaAPI = string.Format(rutaAPI, conversationId);

                var client = InitHttpClient();

                // Serializar el objeto a JSON
                var MensajeRequest = new MensajeRequest { userId = userId , message = message};
                var jsonContent = JsonConvert.SerializeObject(MensajeRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(formattedRutaAPI, content);

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

        public static async Task<string> HandleErrorResponse(HttpResponseMessage response)
        {
            string ErrorMessage = "";

            // Captura el mensaje de error en el cuerpo si está disponible
            var errorContent = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(errorContent))
            {
                var errorObject = JsonConvert.DeserializeObject<dynamic>(errorContent);
                ErrorMessage = errorObject.message;
            }

            return $"Error: {ErrorMessage}";
        }

        public class ErrorResponse
        {
            public string XError { get; set; } = string.Empty;
            public string ErrorMessage { get; set; } = "Se produjo un error al consultar el servicio.";
        }
        public class ConversacionRequest
        {
            public string userId { get; set; }
        }
        public class MensajeRequest
        {
            public string userId { get; set; }
            public string message { get; set; }
        }

    }
}
