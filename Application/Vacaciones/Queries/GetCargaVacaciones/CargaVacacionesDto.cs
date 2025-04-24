using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Vacaciones.Queries.GetCargaVacaciones
{
    public class CargaVacacionesDto : IMapFrom<CargaVacaciones>
    {
        public int IDCARGAVACACIONES { get; set; }
        public string NOMBREARCHIVO { get; set; }
        public int CANTIDADREGISTROS { get; set; }
        public int CANTIDADINGRESADOS { get; set; }

        public string CONTENIDOARCHIVO { get; set; }
        public string RESULTADOCARGA { get; set; }
        public string DESCRIPCIONVALIDACION { get; set; }
        public DateTime FechaCarga { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<CargaVacaciones, CargaVacacionesDto>()

                .ForMember(d => d.IDCARGAVACACIONES, opt => opt.MapFrom(s => s.IDCARGAVACACIONES))
                .ForMember(d => d.NOMBREARCHIVO, opt => opt.MapFrom(s => s.NOMBREARCHIVO))
                .ForMember(d => d.CANTIDADREGISTROS, opt => opt.MapFrom(s => s.CANTIDADREGISTROS))
                .ForMember(d => d.CANTIDADINGRESADOS, opt => opt.MapFrom(s => s.CANTIDADINGRESADOS))
                .ForMember(d => d.CONTENIDOARCHIVO, opt => opt.MapFrom(s => s.CONTENIDOARCHIVO))
                .ForMember(d => d.RESULTADOCARGA, opt => opt.MapFrom(s => s.RESULTADOCARGA))
                .ForMember(d => d.FechaCarga, opt => opt.MapFrom(s => s.FECHACREACION))
                .ForMember(d => d.DESCRIPCIONVALIDACION, opt => opt.MapFrom(s => s.DESCRIPCIONVALIDACION));

        }
    }
}
