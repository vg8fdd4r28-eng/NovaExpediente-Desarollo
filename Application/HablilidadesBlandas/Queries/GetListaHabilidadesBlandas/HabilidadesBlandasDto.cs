using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.HablilidadesBlandas.Queries.GetListaHabilidadesBlandas
{
    public class HabilidadesBlandasDto : IMapFrom<HabilidadBlanda>
    {

        public int IdHabilidadBlanda { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabilidadBlanda, HabilidadesBlandasDto>()
                .ForMember(d => d.IdHabilidadBlanda, opt => opt.MapFrom(s => s.IDHABILIDADBLANDA))       
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE));
        }
    }
}
