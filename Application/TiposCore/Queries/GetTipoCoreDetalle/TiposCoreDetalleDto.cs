using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.TiposCore.Queries.GetTipoCoreDetalle
{
    public class TiposCoreDetalleDto : IMapFrom<TipoCore>
    {
        public int IdTipoCore { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<TipoCore, TiposCoreDetalleDto>()

                .ForMember(d => d.IdTipoCore, opt => opt.MapFrom(s => s.IDTIPOCORE))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION));

        }
    }
}
