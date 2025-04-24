using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ExperienciasLenguajes.Queries.GetListaExperienciasLenguajes
{
    public class ExperienciaLenguajeDto : IMapFrom<ExperienciaLenguaje>
    {

        public int IDLENGUAJEPROGRAMACION { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }
        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string LenguajeProgramacion { get; set; }
        public string NivelConocimiento { get; set; }
        public decimal PorcentajeConocimiento { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExperienciaLenguaje, ExperienciaLenguajeDto>()
                .ForMember(d => d.IDLENGUAJEPROGRAMACION, opt => opt.MapFrom(s => s.IDLENGUAJEPROGRAMACION))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.IDNIVELCONOCIMIENTO, opt => opt.MapFrom(s => s.IDNIVELCONOCIMIENTO))
                .ForMember(d => d.MESESEXPERIENCIA, opt => opt.MapFrom(s => s.MESESEXPERIENCIA))
                .ForMember(d => d.LenguajeProgramacion, opt => opt.MapFrom(s => s.LenguajeProgramacion.NOMBRE))
                .ForMember(d => d.NivelConocimiento, opt => opt.MapFrom(s => s.NivelConocimiento.NOMBRE.Substring(3, 100)))
                .ForMember(d => d.PorcentajeConocimiento, opt => opt.MapFrom(s => Convert.ToDecimal(s.NivelConocimiento.NOMBRE.Substring(0, 1)) / 5));
        }


    }
}
