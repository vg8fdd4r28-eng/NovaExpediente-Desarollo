using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovaExpediente.Application.Common.Interfaces;

namespace NovaExpediente.WebUI.Services
{
    public class RolesPermissions : IRolesPermissions
    {
        static Dictionary<string, List<string>> _permissionsByRole = null;
        static string _defaultRole = string.Empty;

        private const string NOMBRESECCION = "ConfiguracionPermisos";
        private const string SECCIONROLEDEFAULT = "Variables:DefaultRole";

        public RolesPermissions(IConfiguration configuration)
        {
            _permissionsByRole = new Dictionary<string, List<string>>();

            var permissionsSection = configuration.GetSection(NOMBRESECCION);

            foreach (var section in permissionsSection.GetChildren())
            {
                _permissionsByRole.Add(section.Key, section.Get<List<string>>());
            }

            _defaultRole = configuration.GetSection(SECCIONROLEDEFAULT).Value.ToString();
        }

        public List<string> GetRolePermissions(string roleName)
        {
            if (_permissionsByRole.ContainsKey(roleName))
            {
                return _permissionsByRole[roleName];
            }
            else
                return new List<string>();
        }

        public List<string> GetRoles()
        {
            return new List<string>(_permissionsByRole.Keys);
        }

        public string GetDefaultRole() {
            return _defaultRole;
        }
    }
}
