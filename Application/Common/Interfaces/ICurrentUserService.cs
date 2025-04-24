using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }

        bool IsAuthenticated { get; }

        List<string> GetRoles();
        Task<List<string>> GetPermissions();
    }
}
