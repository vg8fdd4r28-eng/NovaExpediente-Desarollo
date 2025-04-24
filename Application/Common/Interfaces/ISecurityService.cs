using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface ISecurityService
    {

        Task<List<string>> GetUsersWithRole(string roleName);
    }
}
