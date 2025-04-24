using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Queries.GetNoticiasCategorias
{
    public class NoticiasCategoriasDto : IMapFrom<NoticiaCategoria>
    {
        public int IdNoticiaCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo{ get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NoticiaCategoria, NoticiasCategoriasDto>()
                .ForMember(d => d.IdNoticiaCategoria, opt => opt.MapFrom(s => s.IDNOTICIACATEGORIA))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION));
        }
    }
}
