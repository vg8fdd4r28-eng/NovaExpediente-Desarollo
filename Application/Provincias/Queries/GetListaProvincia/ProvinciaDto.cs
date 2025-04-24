using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Provincias.Queries.GetListaProvincia
{
    public class ProvinciaDto : IMapFrom<Provincia>
    {

        public int IDPROVINCIA { get; set; }
        public string NOMBRE { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<Provincia, ProvinciaDto>()
                .ForMember(d => d.IDPROVINCIA, opt => opt.MapFrom(s => s.IDPROVINCIA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE));
        }


    }
}

