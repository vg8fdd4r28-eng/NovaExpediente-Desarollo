using NovaExpediente.Application.Nomina.Queries.GetSaldoVacaciones;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Net = System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using NovaExpediente.Application.Common.Models;
using System.Globalization;

namespace NovaExpediente.Infrastructure.Curriculum
{
    public class NominaService : INominaService
    {
        private readonly IConfiguration Configuration;
        private readonly ILoggingService _loggingService;

        public NominaService(IConfiguration configuration, ILoggingService loggingService)
        {
            Configuration = configuration;
            _loggingService = loggingService;
        }



        private HttpClient InitHttpClient() 
        {
            string baseAddress = Configuration.GetSection("Nomina").GetSection("BaseUrlApiNomina").Value;

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };


            var client = new HttpClient(handler);
            //var client = new HttpClient(); //with SSL Check 

            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client; 
        }    


        public async Task<string> Login()
        {
            string result = "";


            string baseAddress = Configuration.GetSection("Nomina").GetSection("BaseUrlApiNomina").Value;
            string rutaAPI = Configuration.GetSection("Nomina").GetSection("RutaAPILogin").Value;

            string userNameLogin = Configuration.GetSection("Nomina").GetSection("UserNameLogin").Value;
            string passwordLogin = Configuration.GetSection("Nomina").GetSection("PasswordLogin").Value;
            string companyDBLogin = Configuration.GetSection("Nomina").GetSection("CompanyDBLogin").Value;


            var client = InitHttpClient();


            var credential = new VacacionesCredentials { UserName = userNameLogin, Password = passwordLogin, CompanyDB = companyDBLogin };
            var credentialString = Newtonsoft.Json.JsonConvert.SerializeObject(credential, Newtonsoft.Json.Formatting.None);
            var content = new StringContent(credentialString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(rutaAPI, content);
            if (response.IsSuccessStatusCode)
            { result = await response.Content.ReadAsStringAsync(); }
            else
            {
                result = "Se produjo un error en el login.";
            }

            return result;
        }

        public async Task<string> LogOut(string session)
        {
            string result = "";


            string baseAddress = Configuration.GetSection("Nomina").GetSection("BaseUrlApiNomina").Value;
            string rutaAPI = Configuration.GetSection("Nomina").GetSection("RutaAPILogOut").Value;

            var client = InitHttpClient();
            client.DefaultRequestHeaders.Add("SessionId", session);
            var content = new StringContent("", Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(rutaAPI, content);
            if (response.IsSuccessStatusCode)
            { result = await response.Content.ReadAsStringAsync(); }
            else
            {
                result = "Se produjo un error en el logout.";
            }

            return result;
        }

        public async Task<string> ConsultaSaldo(int? IdNomina)
        {
            string result = "";

            string login = await Login();

            VacacionesNominaVm session = JsonConvert.DeserializeObject<VacacionesNominaVm>(login);


            string baseAddress = Configuration.GetSection("Nomina").GetSection("BaseUrlApiNomina").Value;
            string rutaAPI = Configuration.GetSection("Nomina").GetSection("RutaAPIConsultaSaldoVacaciones").Value;


            var client = InitHttpClient();
            
            client.DefaultRequestHeaders.Add("SessionId", session.SessionId);


            var response = await client.GetAsync($"{rutaAPI}?key={IdNomina}");

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                result = "Se produjo un error en la consulta de saldos.";
            }


            await LogOut(session.SessionId);


            return result;
        }

        /// <summary>
        /// Obtiene el saldo de vacaciones para cada colaborador en la lista de identificadores.
        /// Se obtiene el número de sesion para conectarse al servicio y luego se consulta el 
        /// saldo de cada colaborador
        /// </summary>
        /// <param name="identificadoresNomina">Diccionario en donde la llave es el identificador del colaborador en el 
        /// Expediente Digital y el valor es el identificador del colaborador en el sistema de Nómina</param>
        /// <returns></returns>
        public async Task<Dictionary<int, decimal>> ConsultaSaldo(Dictionary<int, int?> identificadoresNomina)
        {
            string result = "";
            string login = await Login();

            VacacionesNominaVm session = JsonConvert.DeserializeObject<VacacionesNominaVm>(login);
            Dictionary<int, decimal> saldos = new Dictionary<int, decimal>();

            
            string rutaAPI = Configuration.GetSection("Nomina").GetSection("RutaAPIConsultaSaldoVacaciones").Value;

            var client = InitHttpClient();
            
            client.DefaultRequestHeaders.Add("SessionId", session.SessionId);

            foreach (var id in identificadoresNomina.Keys)
            {
                var response = await client.GetAsync($"{rutaAPI}?key={identificadoresNomina[id].Value}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    Dictionary<string, object> saldo = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);

                    //var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                    //var provider = new CultureInfo("en-EN");

                    //saldos.Add(id, decimal.Parse(saldo["diasDisponibles"].ToString(), style, provider));
                    saldos.Add(id, decimal.Parse(saldo["diasDisponibles"].ToString()));

                }
                else
                {
                    result = "Se produjo un error en la consulta de saldos.";
                    saldos.Add(id, 0);
                }
            }

            await LogOut(session.SessionId);

            return saldos;
        }
        /// <summary>
        /// Envía datos de una solicitud de vacaciones aprobada al sistema de nómina
        /// para que genere automáticamente la acción de personal correspondiente
        /// </summary>
        /// <param name="IdNomina">Identificador del colaborador en el sistema de nómina</param>
        /// <param name="FechaInicio">Fecha de inicio del periodo de vacaciones</param>
        /// <param name="FechaFinal">Fecha final del periodo de vacaciones</param>
        /// <param name="CantDias">Cantidad de días del periodo de vacaciones</param>
        /// <param name="numeroSolicitud">Número de solicitud administrativa por vacaciones en el Expediente Digital</param>
        /// <returns></returns>
        public async Task<string> ActualizaSaldoVacaciones(int? IdNomina, DateTime FechaInicio, DateTime FechaFinal, int CantDias, long numeroSolicitud)
        {
            string result = "";

            // obtener número de sesión para enviar los datos de la solicitud de vacaciones
            string login = await Login();
            VacacionesNominaVm session = JsonConvert.DeserializeObject<VacacionesNominaVm>(login);

            // se obtiene la dirección del servicio
            string baseAddress = Configuration.GetSection("Nomina").GetSection("BaseUrlApiNomina").Value;
            string rutaAPI = Configuration.GetSection("Nomina").GetSection("RutaAPIActualizaSaldoVacaciones").Value;

            // configuración del cliente HTTP
            var client = InitHttpClient();
            
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
            

            client.DefaultRequestHeaders.Add("SessionId", session.SessionId);

            // datos de la solicitud
            var credential = new VacacionesActualizarCredentials
            {
                EmpId = IdNomina,
                FechaEntrada = FechaFinal,
                FechaSalida = FechaInicio,
                DiasGozados = CantDias.ToString(),
                Aprobadas = true,
                Pagadas = true,
                Comentarios = $"NovaExpediente - Registro automático tras aprobar la solicitud de vacaciones #{numeroSolicitud}",
                IsFraccion = false,
                Fraccion = "1",
                UsuarioIns = 1
            };

            // serialización de la solicitud y se eliminan datos no requeridos como resultado de la serialización
            var solicitudString = Newtonsoft.Json.JsonConvert.SerializeObject(credential, Newtonsoft.Json.Formatting.Indented);
            solicitudString = solicitudString.Replace("T00:00:00", "");
            solicitudString = solicitudString.Replace("True", "true");

            // se empaqueta la solicitud para enviarla al API de Nómina
            var buffer = System.Text.Encoding.UTF8.GetBytes(solicitudString);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // se envía la solicitud a Nómina
            HttpResponseMessage response = await client.PostAsync(rutaAPI, byteContent);
            if (response.IsSuccessStatusCode)
            {
                result ="OK:" +  await response.Content.ReadAsStringAsync();
                _loggingService.LogInformation(EnumLoggingCategory.Nomina, $"ActualizaSaldoVacaciones - {result}", (int)numeroSolicitud);
            }
            else
            {
                result = "ER:" + response.ToString();
                _loggingService.LogError(EnumLoggingCategory.Nomina, $"ActualizaSaldoVacaciones - {result}", (int)numeroSolicitud);
            }

            // se cierra la sesión con el API de Nómina
            await LogOut(session.SessionId);

            return result;
        }



    }

    public class VacacionesCredentials
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyDB { get; set; }
    }

    public class VacacionesActualizarCredentials
    {

        public int? EmpId { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaEntrada { get; set; }
        public string DiasGozados { get; set; }
        public int UsuarioIns { get; set; }
        public bool Pagadas { get; set; }
        public bool Aprobadas { get; set; }
        public string Comentarios { get; set; }
        public bool IsFraccion { get; set; }
        public string Fraccion { get; set; }



    }

}
