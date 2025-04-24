using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Distritos.Queries.GetListaDistrito
{
    public class DistritoDto : IMapFrom<Distrito>
    {

        public int IDDISTRITO { get; set; }
        public string NOMBRE { get; set; }
        public int IDCANTON { get; set; }
        public string CANTONNOMBRE { get; set; }
        public int IDPROVINCIA { get; set; }
        public string PROVINCIANOMBRE { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Distrito, DistritoDto>()
                .ForMember(d => d.IDDISTRITO, opt => opt.MapFrom(s => s.IDDISTRITO))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                 .ForMember(d => d.IDCANTON, opt => opt.MapFrom(s => s.IDCANTON))
                .ForMember(d => d.CANTONNOMBRE, opt => opt.MapFrom(s => s.Canton.NOMBRE))
                .ForMember(d => d.IDCANTON, opt => opt.MapFrom(s => s.Canton.Provincia.IDPROVINCIA))
                .ForMember(d => d.PROVINCIANOMBRE, opt => opt.MapFrom(s => s.Canton.Provincia.NOMBRE));
        }


    }
}

