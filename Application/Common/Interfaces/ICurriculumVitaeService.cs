using System.Threading.Tasks;
using static NovaExpediente.Application.CurriculumVitae.Commands.CargarCVCommand;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface ICurriculumVitaeService
    {
        Task<(string uuid, string estado, string mensaje)> CargarCVAsync(ModeloCarga document);
        Task<(string estado, string mensaje)> ActualizarCVAsync(ModeloCarga document);
    }
}
