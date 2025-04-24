using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ProyectoLenguajes.Queries.GetListaProyectoLenguaje
{
    public class ProyectoLenguajeDto : IMapFrom<ProyectoLenguaje>
    {

        public int IDPROYECTOLENGUAJE { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDLENGUAJEPROGRAMACION { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        public string LenguajeProgramacion { get; set; }
     


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProyectoLenguaje, ProyectoLenguajeDto>()
                .ForMember(d => d.IDPROYECTOLENGUAJE, opt => opt.MapFrom(s => s.IDPROYECTOLENGUAJE))
                .ForMember(d => d.IDPROYECTO, opt => opt.MapFrom(s => s.IDPROYECTO))
                .ForMember(d => d.IDLENGUAJEPROGRAMACION, opt => opt.MapFrom(s => s.IDLENGUAJEPROGRAMACION))
                .ForMember(d => d.LenguajeProgramacion, opt => opt.MapFrom(s => s.LenguajeProgramacion.NOMBRE));
        }


    }
}
