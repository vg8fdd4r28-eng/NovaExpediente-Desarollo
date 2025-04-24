using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.UnoAUnoEstadosColaboradores.Queries.GetListaUnoAUnoEstadosColaboradores
{
    public class UnosAUnosEstadosColaboradoresDto : IMapFrom<UnoAUnoEstadoColaborador>
    {
        public int IdUnoAUnoEstadoColaborador { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UnoAUnoEstadoColaborador, UnosAUnosEstadosColaboradoresDto>()
                .ForMember(d => d.IdUnoAUnoEstadoColaborador, opt => opt.MapFrom(s => s.IDUNOAUNOESTADOCOLABORADOR))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO));
        }

    }

}
