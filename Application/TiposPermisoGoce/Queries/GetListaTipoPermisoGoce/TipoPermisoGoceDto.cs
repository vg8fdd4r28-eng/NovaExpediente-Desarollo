using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.TiposPermisoGoce.Queries.GetListaTipoPermisoGoce
{
    public class TipoPermisoGoceDto : IMapFrom<TipoPermisoGoce>
    {
        public int IDTIPOPERMISOGOCE { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ESTADO { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<TipoPermisoGoce, TipoPermisoGoceDto>()

                .ForMember(d => d.IDTIPOPERMISOGOCE, opt => opt.MapFrom(s => s.IDTIPOPERMISOGOCE))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.ESTADO, opt => opt.MapFrom(s => s.ESTADO))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));

        }
    }
}
