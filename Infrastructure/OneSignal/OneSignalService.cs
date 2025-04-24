using Microsoft.Extensions.Configuration;
using NovaExpediente.Application.Common.Interfaces;
using RestSharp;
using System.Threading.Tasks;

namespace NovaExpediente.Infrastructure.CloudService
{
    public class OneSignalService : IOneSignalService
    {
        private readonly IConfiguration Configuration;
        private string REST_API_KEY;
        private string APP_ID;

        public OneSignalService(IConfiguration configuration)
        {
            Configuration = configuration;

            REST_API_KEY = Configuration.GetSection("OneSignal").GetSection("REST_API_KEY").Value;
            APP_ID = Configuration.GetSection("OneSignal").GetSection("APP_ID").Value;


        }


        public async Task<string> ViewDevices()
        {



            var client = new RestClient("https://onesignal.com/api/v1/players?app_id=" + APP_ID + "&limit=300&offset=0");
            var request = new RestRequest("", Method.Get);
            request.AddHeader("Accept", "text/plain");
            request.AddHeader("Authorization", "Basic " + REST_API_KEY);

            RestResponse response = await client.ExecuteAsync(request);

            return response.Content;


        }

        public async Task<string> DeleteDevice(string Id)
        {


            var client = new RestClient("https://onesignal.com/api/v1/players/" + Id + "?app_id=" + APP_ID);
            var request = new RestRequest("", Method.Delete);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Basic " + REST_API_KEY);

            RestResponse response = await client.ExecuteAsync(request);

            return response.Content;

        }

        public async Task<string> ViewNotifications()
        {


            var client = new RestClient("https://onesignal.com/api/v1/notifications?app_id=" + APP_ID + "&limit=300&offset=0&kind=1");
            var request = new RestRequest("", Method.Get);
            request.AddHeader("Accept", "text/plain");
            request.AddHeader("Authorization", "Basic " + REST_API_KEY);

            RestResponse response = await client.ExecuteAsync(request);

            return response.Content;


        }

        public async Task<string> SendNotification(string titulo, string msj, string[] segmentos, string todos)
        {
            var client = new RestClient("https://onesignal.com/api/v1/notifications?app_id=" + APP_ID + "&limit=300&offset=0");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Basic " + REST_API_KEY);
            request.AddHeader("Content-Type", "application/json");

            string data = "";

            if (todos == "Si")
            {
                data = $"[\r\n   {{\r\n      \"field\": \"tag\",\r\n      \"key\": \"cliente\",\r\n      \"relation\": \"!=\",\r\n      \"value\": \"ALL\"\r\n   }}\r\n]";
            }
            else
            {
                data += "[";
                for (int i = 0; i < segmentos.Length; i++)
                {
                    data += $"{"{'field': 'tag', 'key': 'cliente', 'relation': '=', 'value':" + "'" + segmentos[i].Trim() + "'},"}";
                    data += $"{"{'operator': 'OR'},"}";
                }
                data = data.Remove(data.Length - 20, 20).Replace("'", "\"");
                data += "]";
            }

            request.AddParameter("application/json", "{\"filters\":" + data + "," +
                           "\"contents\":{\"en\":\"" + msj + "\",\"es\":\"" + msj + "\"}," +
                           "\"headings\":{\"en\":\"" + titulo + "\",\"es\":\"" + titulo + "\"}," +
                           "\"name\":\"NovaExpediente\"}", ParameterType.RequestBody);

            RestResponse response = await client.ExecuteAsync(request);

            return response.Content;

        }





    }
}
