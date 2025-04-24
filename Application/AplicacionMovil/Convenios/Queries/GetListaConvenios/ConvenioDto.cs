using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.AplicacionMovil.Convenios.Queries.GetListaConvenios
{
    public class ConvenioDto : IMapFrom<Convenio>
    {


        public int IdConvenio { get; set; }
        public int IdConvenioCategoria { get; set; }
        public int IdIdioma { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public string Ubicacion { get; set; }
        public bool Activo { get; set; }
        public string Idiomas { get; set; }
        public string Categorias { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Convenio, ConvenioDto>()
            .ForMember(d => d.IdConvenio, opt => opt.MapFrom(s => s.IDCONVENIO))
            .ForMember(d => d.IdConvenioCategoria, opt => opt.MapFrom(s => s.IDCONVENIOCATEGORIA))
            .ForMember(d => d.IdIdioma, opt => opt.MapFrom(s => s.IDIDIOMA))
            .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
            .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION))
            .ForMember(d => d.Telefono, opt => opt.MapFrom(s => s.TELEFONO))
            .ForMember(d => d.Ubicacion, opt => opt.MapFrom(s => s.UBICACION))
            .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO))
            .ForMember(d => d.Idiomas, opt => opt.MapFrom(s => s.Idioma.NOMBRE))
            .ForMember(d => d.Categorias, opt => opt.MapFrom(s => s.ConvenioCategoria.NOMBRE));
        }
    }
}
