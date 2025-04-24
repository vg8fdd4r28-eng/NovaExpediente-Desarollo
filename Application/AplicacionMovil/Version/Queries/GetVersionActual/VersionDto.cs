using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.AplicacionMovil.Version.Queries.GetVersionActual
{
    public class VersionDto : IMapFrom<CatalogoValor>
    {
        public int IdCatalogoValor { get; set; }
        public string CodCatalogo { get; set; }
        public string CodCatalogoValor { get; set; }
        public string Detalle { get; set; }
        public string Descripcion { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
        public string Valor3 { get; set; }
        public string Valor4 { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CatalogoValor, VersionDto>()
                .ForMember(d => d.IdCatalogoValor, opt => opt.MapFrom(s => s.IDCATALOGOVALOR))
                .ForMember(d => d.CodCatalogo, opt => opt.MapFrom(s => s.CODCATALOGO))
                .ForMember(d => d.CodCatalogoValor, opt => opt.MapFrom(s => s.CODCATALOGOVALOR))
                .ForMember(d => d.Detalle, opt => opt.MapFrom(s => s.DETALLE))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.Valor1, opt => opt.MapFrom(s => s.VALOR1))
                .ForMember(d => d.Valor2, opt => opt.MapFrom(s => s.VALOR2))
                .ForMember(d => d.Valor3, opt => opt.MapFrom(s => s.VALOR3))
                .ForMember(d => d.Valor4, opt => opt.MapFrom(s => s.VALOR4));
        }
    }
}
