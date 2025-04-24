using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.AplicacionMovil.Actividades.Queries.GetListaActividades
{
    public class ActividadDto : IMapFrom<Actividad>
    {

        public int IDACTIVIDAD { get; set; }
        public int IDIDIOMA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FECHA { get; set; }
        public string ENLACE { get; set; }
        public bool ACTIVO { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }
        public string Idiomas { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<Actividad, ActividadDto>()
            .ForMember(d => d.IDACTIVIDAD, opt => opt.MapFrom(s => s.IDACTIVIDAD))
            .ForMember(d => d.IDIDIOMA, opt => opt.MapFrom(s => s.IDIDIOMA))
            .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
            .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
            .ForMember(d => d.FECHA, opt => opt.MapFrom(s => s.FECHA))
            .ForMember(d => d.ENLACE, opt => opt.MapFrom(s => s.ENLACE))
            .ForMember(d => d.ACTIVO, opt => opt.MapFrom(s => s.ACTIVO))
            .ForMember(d => d.LAT, opt => opt.MapFrom(s => s.LAT))
            .ForMember(d => d.LNG, opt => opt.MapFrom(s => s.LNG))
            .ForMember(d => d.Idiomas, opt => opt.MapFrom(s => s.Idioma.NOMBRE));

        }
    }
}
