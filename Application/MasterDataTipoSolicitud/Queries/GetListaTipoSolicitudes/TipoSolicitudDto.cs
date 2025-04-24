using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.MasterDataTipoSolicitud.Queries.GetListaTipoSolicitudes
{
    public class TipoSolicitudDto : IMapFrom<TipoSolicitud>
    {
        public int IDTIPOSOLICITUD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBRECATEGORIA { get; set; }
        public string CATEGORIA { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TipoSolicitud, TipoSolicitudDto>()
                           .ForMember(d => d.IDTIPOSOLICITUD, opt => opt.MapFrom(s => s.IDTIPOSOLICITUD))
                           .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                           .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                           .ForMember(d => d.NOMBRECATEGORIA, opt => opt.MapFrom(s => s.CATEGORIA == "MD" ? "Master Data" : "Administrativa"))
                           .ForMember(d => d.CATEGORIA, opt => opt.MapFrom(s => s.CATEGORIA));
        }
    }
}
