using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Net = System.Net;

namespace NovaExpediente.Infrastructure.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _configuration;

        // contexto del request para obtener el Access Token del usuario
        IHttpContextAccessor _httpContext;
        public SecurityService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContext = httpContextAccessor;
            _configuration = configuration;
        }
        /// <summary>
        /// Obtiene la lista de cuentas de correo de los usuarios
        /// que tienen asignado un rol específico (roleName). 
        /// Primero obtiene el identificador del rol en cuestión
        /// y luego utiliza otro método para obtener la lista de correos
        /// </summary>
        /// <param name="roleName">Nombre del rol</param>
        /// <returns>Lista de correos</returns>
        public async Task<List<string>> GetUsersWithRole(string roleName)
        {
            List<string> users = null;

            string baseAddress = _configuration.GetSection("IdentityServerAPI").GetSection("BaseUrlApi").Value;  // "https://localhost:44302";
            string rutaAPIRoles = _configuration.GetSection("IdentityServerAPI").GetSection("RutaAPIRoles").Value; // "/api/Roles";
            var accessToken = _httpContext.HttpContext.Request.Headers["Authorization"].ToString().Split(' ');

            // configuración del cliente HTTP
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessToken[0], accessToken[1]);
           
            // se invoca al servicio del IDENTITYSERVER para obtener el ID de ROLENAME
            var response = await client.GetAsync($"{rutaAPIRoles}?searchText={roleName}&page=1&pageSize=1");
            
            //HttpResponseMessage response = await client.GetAsync(rutaAPI, content);

            // si la llamada es exitosa se obtiene el ID del role
            if (response.IsSuccessStatusCode)
            {
                // estructura JSON con el formato
                //   {
                //     "pageSize": 0,
                //     "totalCount": 0,
                //     "roles": [
                //       {
                //                           "name": "string",
                //         "id": "string"
                //       }
                //     ]
                //   }
                var responseInfo = await response.Content.ReadAsStringAsync();

                // se procesa el resultado
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseInfo);

                if (int.Parse(result["totalCount"].ToString()) > 0)
                {
                    var roles = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result["roles"].ToString());

                    var role = roles[0];
                    users = await GetUsers(client, role["id"].ToString());
                }
            }

            return users;
        }

        /// <summary>
        /// Obtiene la lista de correos de los usuarios que tienen asignado un rol
        /// </summary>
        /// <param name="client">Cliente HTTP</param>
        /// <param name="roleId">Identificador del rol</param>
        /// <returns></returns>
        private async Task<List<string>> GetUsers(HttpClient client, string roleId)
        {
            List<string> userList = null;
            string rutaAPIUsuariosRol = _configuration.GetSection("IdentityServerAPI").GetSection("RutaAPIUsersRole").Value; //"/api/Roles/{0}/Users?page=1&pageSize=100";
            var response = await client.GetAsync(string.Format(rutaAPIUsuariosRol + "?page=1&pageSize=100", roleId));
            var responseInfo = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                // estructuta JSON con el formato
                //   {
                //       "pageSize": 0,
                //       "totalCount": 0,
                //       "users": [
                //       {
                //           "userName": "string",
                //           "email": "user@example.com",
                //           "emailConfirmed": true,
                //           "phoneNumber": "string",
                //           "phoneNumberConfirmed": true,
                //           "lockoutEnabled": true,
                //           "twoFactorEnabled": true,
                //           "accessFailedCount": 0,
                //           "lockoutEnd": "2021-06-21T23:00:04.338Z",
                //           "id": "string"
                //       }
                //       ]
                //   }
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseInfo);

                if (int.Parse(result["totalCount"].ToString()) > 0)
                {
                    var users = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result["users"].ToString());

                    userList = new List<string>();
                    foreach (var user in users)
                    {
                        userList.Add(user["email"].ToString());
                    } 
                }
            }
            return userList;
        }
    }
}

