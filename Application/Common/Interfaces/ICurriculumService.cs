using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface ICurriculumService
    {
        Dictionary<string, object> GenerarCV(string JsonCV, int TipoCV);
    }
}
