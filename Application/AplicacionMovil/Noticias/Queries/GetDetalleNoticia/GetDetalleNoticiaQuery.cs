using MediatR;

namespace NovaExpediente.Application.AplicacionMovil.Noticias.Queries.GetDetalleNoticia
{
    public class GetDetalleNoticiaQuery: IRequest<string>
    {
        public int IdNoticia { get; set; }
    }
}
