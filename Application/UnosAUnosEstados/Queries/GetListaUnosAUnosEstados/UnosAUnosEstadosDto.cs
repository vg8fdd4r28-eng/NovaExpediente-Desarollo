using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.UnosAUnosEstados.Queries.GetListaUnosAUnosEstados
{
    public class UnosAUnosEstadosDto :IMapFrom<UnoAUnoEstado>
    {
        public int IdUnoAUnoEstado { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UnoAUnoEstado, UnosAUnosEstadosDto>()
                .ForMember(d => d.IdUnoAUnoEstado, opt => opt.MapFrom(s => s.IDUNOAUNOESTADO))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO));
        }
    }
}
