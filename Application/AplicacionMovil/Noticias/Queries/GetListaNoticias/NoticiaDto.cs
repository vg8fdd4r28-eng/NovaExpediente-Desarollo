using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.AplicacionMovil.Noticias.Queries.GetListaNoticias
{
    public class NoticiaDto : IMapFrom<Noticia>
    {
        public int IdNoticia { get; set; }
        public int IdNoticiaCategoria { get; set; }
        public int IdIdioma { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public bool Activo { get; set; }
        public string CorreoAMostrar { get; set; }
        public string NombreAMostrar { get; set; }
        public bool EsPublica { get; set; }
        public int IdAutor { get; set; }
        public string Idiomas { get; set; }
        public string Categorias { get; set; }
        public string Publicas { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Noticia, NoticiaDto>()
            .ForMember(d => d.IdNoticia, opt => opt.MapFrom(s => s.IDNOTICIA))
            .ForMember(d => d.IdNoticiaCategoria, opt => opt.MapFrom(s => s.IDNOTICIACATEGORIA))
            .ForMember(d => d.IdIdioma, opt => opt.MapFrom(s => s.IDIDIOMA))
            .ForMember(d => d.FechaVencimiento, opt => opt.MapFrom(s => s.FECHAVENCIMIENTO))
            .ForMember(d => d.Titulo, opt => opt.MapFrom(s => s.TITULO))
            .ForMember(d => d.Contenido, opt => opt.MapFrom(s => s.CONTENIDO))
            .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO))
            .ForMember(d => d.CorreoAMostrar, opt => opt.MapFrom(s => s.CORREOAMOSTRAR))
            .ForMember(d => d.NombreAMostrar, opt => opt.MapFrom(s => s.NOMBREAMOSTRAR))
            .ForMember(d => d.EsPublica, opt => opt.MapFrom(s => s.ESPUBLICA))
            .ForMember(d => d.IdAutor, opt => opt.MapFrom(s => s.IDUSUARIOCREA))
            .ForMember(d => d.Idiomas, opt => opt.MapFrom(s => s.Idioma.NOMBRE))
            .ForMember(d => d.Categorias, opt => opt.MapFrom(s => s.NoticiaCategoria.NOMBRE))
            .ForMember(d => d.Publicas, opt => opt.MapFrom(s => s.ESPUBLICA == true ? "Pública" : "Privada"));
        }
    }
}
