using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Buses.Queries.GetListaBus
{
    public class BusDto : IMapFrom<Bus>
    {
        public int IdBus { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bus, BusDto>()
               .ForMember(d => d.IdBus, opt => opt.MapFrom(s => s.IdBus))
               .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre))
               .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.Descripcion));
        }


    }
}

