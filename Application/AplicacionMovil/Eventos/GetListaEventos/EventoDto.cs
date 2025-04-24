using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;

namespace NovaExpediente.Application.AplicacionMovil.Eventos.GetListaEventos
{
    public class EventoDto : IMapFrom<Evento>
    {
        public int IDEVENTO { get; set; }
        public int IDIDIOMA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FECHA { get; set; }
        public string ENLACE { get; set; }
        public bool ACTIVO { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }

        public List<EventoSeccionDto> EventoSecciones { get; set; }

        public List<ImagenDto> Imagenes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Evento, EventoDto>()
                .ForMember(d => d.IDEVENTO, opt => opt.MapFrom(s => s.IDEVENTO))
                .ForMember(d => d.IDIDIOMA, opt => opt.MapFrom(s => s.IDIDIOMA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.FECHA, opt => opt.MapFrom(s => s.FECHA))
                .ForMember(d => d.ENLACE, opt => opt.MapFrom(s => s.ENLACE))
                .ForMember(d => d.ACTIVO, opt => opt.MapFrom(s => s.ACTIVO))
                .ForMember(d => d.LAT, opt => opt.MapFrom(s => s.LAT))
                .ForMember(d => d.LNG, opt => opt.MapFrom(s => s.LNG))
                .ForMember(d => d.EventoSecciones, opt => opt.MapFrom(s => s.EventoSecciones))
                .ForMember(d => d.Imagenes, opt => opt.MapFrom(s => s.Imagenes));
        }
    }
}
