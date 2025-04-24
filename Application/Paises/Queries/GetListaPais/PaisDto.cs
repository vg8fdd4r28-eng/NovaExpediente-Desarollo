using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Paises.Queries.GetListaPais
{
    public class PaisDto : IMapFrom<Pais>
    {

        public int IDPAIS { get; set; }
        public string NOMBRE { get; set; }

        public string CODPAIS { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pais, PaisDto>()
                .ForMember(d => d.IDPAIS, opt => opt.MapFrom(s => s.IDPAIS))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.CODPAIS, opt => opt.MapFrom(s => s.CODPAIS));
                
        }


    }
}

