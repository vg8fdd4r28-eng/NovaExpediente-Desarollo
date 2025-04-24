using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Cantones.Queries.GetListaCanton
{
    public class CantonDto : IMapFrom<Canton>
    {

        public int IDCANTON { get; set; }
        public string NOMBRE { get; set; }
        public int IDPROVINCIA { get; set; }
        public string PROVINCIANOMBRE { get; set; }




        public void Mapping(Profile profile)
        {
            profile.CreateMap<Canton, CantonDto>()
                .ForMember(d => d.IDCANTON, opt => opt.MapFrom(s => s.IDCANTON))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.IDPROVINCIA, opt => opt.MapFrom(s => s.IDPROVINCIA))
                .ForMember(d => d.PROVINCIANOMBRE, opt => opt.MapFrom(s => s.Provincia.NOMBRE));
        }


    }
}

