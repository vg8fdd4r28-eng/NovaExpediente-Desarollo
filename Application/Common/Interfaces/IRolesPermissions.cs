using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface IRolesPermissions
    {
        List<string> GetRolePermissions(string roleName);
        List<string> GetRoles();
        string GetDefaultRole();
    }
}
