using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.AreasOrganizativas.Queries.GetAreaOrganizativaDetalle
{
    public class AreaOrganizativaDetalleDto : IMapFrom<AreaOrganizativa>
    {
        public int IDAREAORGANIZATIVA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


        public void Mapping(Profile profile)
        {


            profile.CreateMap<AreaOrganizativa, AreaOrganizativaDetalleDto>()
                .ForMember(d => d.IDAREAORGANIZATIVA, opt => opt.MapFrom(s => s.IDAREAORGANIZATIVA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }
    }
}
