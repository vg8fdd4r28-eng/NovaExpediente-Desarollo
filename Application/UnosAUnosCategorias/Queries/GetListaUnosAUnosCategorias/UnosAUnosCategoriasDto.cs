using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.UnosAUnosCategorias.Queries.GetListaUnosAUnosCategorias
{
    public class UnosAUnosCategoriasDto : IMapFrom<UnoAUnoCategoria>
    {
        public int IdUnoAUnoCategoria { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UnoAUnoCategoria, UnosAUnosCategoriasDto>()
                .ForMember(d => d.IdUnoAUnoCategoria, opt => opt.MapFrom(s => s.IDUNOAUNOCATEGORIA))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO));
        }
    }
}
