using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.MasterDataTipoSolicitud.Queries.GetDetalleTipoSolicitud
{
    public class TipoSolicitudDetalleDto : IMapFrom<TipoSolicitud>
    {
        public int? IDTIPOSOLICITUD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TipoSolicitud, TipoSolicitudDetalleDto>()

                           .ForMember(d => d.IDTIPOSOLICITUD, opt => opt.MapFrom(s => s.IDTIPOSOLICITUD))
                           .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                           .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));

        }
    }
}
