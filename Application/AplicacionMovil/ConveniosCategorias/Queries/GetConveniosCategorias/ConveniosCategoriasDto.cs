using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.AplicacionMovil.ConveniosCategorias.Queries.GetConveniosCategorias
{
    public class ConveniosCategoriasDto : IMapFrom<ConvenioCategoria>
    {
        public int IdConvenioCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo{ get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ConvenioCategoria, ConveniosCategoriasDto>()
                .ForMember(d => d.IdConvenioCategoria, opt => opt.MapFrom(s => s.IDCONVENIOCATEGORIA))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION));
        }
    }
}
