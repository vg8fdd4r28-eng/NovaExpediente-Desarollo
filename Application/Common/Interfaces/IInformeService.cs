using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface IInformeService
    {
        Task<string> ConsultaConversaciones(string userId, int currentPage);
        Task<string> ConsultaConversacionDetalle(string conversationId);
        Task<string> CrearConversacion(string userId);
        Task<string> CrearMensaje(string userId, string conversationId, string message);
    }
}