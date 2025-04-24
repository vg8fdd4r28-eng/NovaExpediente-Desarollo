using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace NovaExpediente.Application.AplicacionMovil.Eventos.GetListaEventos
{
    public class EventoSeccionDto : IMapFrom<EventoSeccion>
    {
        public int IDEVENTOSECCION { get; set; }

        [Required, StringLength(50)]
        public string NOMBRE { get; set; }

        [Required, StringLength(100)]
        public string TITULO { get; set; }

        [Required, StringLength(50)]
        public string TIPO { get; set; }

        [Required]
        public string[] OPCIONES { get; set; }

        [Required]
        public bool ESREQUERIDO { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<EventoSeccion, EventoSeccionDto>()
              .ForMember(d => d.IDEVENTOSECCION, opt => opt.MapFrom(s => s.IDEVENTOSECCION))
              .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
              .ForMember(d => d.TITULO, opt => opt.MapFrom(s => s.TITULO))
              .ForMember(d => d.TIPO, opt => opt.MapFrom(s => s.TIPO))
              .ForMember(d => d.OPCIONES, opt => opt.MapFrom(s => s.OPCIONES.Split(',', StringSplitOptions.RemoveEmptyEntries)))
              .ForMember(d => d.ESREQUERIDO, opt => opt.MapFrom(s => s.ESREQUERIDO));
        }
    }
}
