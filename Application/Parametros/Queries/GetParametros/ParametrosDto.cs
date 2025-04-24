using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using System;

namespace NovaExpediente.Application.Parametros.Queries.GetParametros
{
    public class ParametrosDto : IMapFrom<Domain.Entities.Parametros>
    {
        public int IdParametro { get; set; }

        public Int16 IndicadorSolicitudVerde { get; set; }
        public Int16 IndicadorSolicitudAmarillo { get; set; }
        public Int16 IndicadorSolicitudRojo { get; set; }
        public Int16 IndicadorExpedienteVerde { get; set; }
        public Int16 IndicadorExpedienteAmarillo { get; set; }
        public Int16 IndicadorExpedienteRojo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Parametros, ParametrosDto>()
                .ForMember(d => d.IdParametro, opt => opt.MapFrom(s => s.IDPARAMETROS))
                .ForMember(d => d.IndicadorSolicitudVerde, opt => opt.MapFrom(s => s.INDICADOR_MASTERDATA_VERDE))
                .ForMember(d => d.IndicadorSolicitudAmarillo, opt => opt.MapFrom(s => s.INDICADOR_MASTERDATA_AMARILLO))
                .ForMember(d => d.IndicadorSolicitudRojo, opt => opt.MapFrom(s => s.INDICADOR_MASTERDATA_ROJO))
                .ForMember(d => d.IndicadorExpedienteVerde, opt => opt.MapFrom(s => s.INDICADOR_EXPEDIENTE_VERDE))
                .ForMember(d => d.IndicadorExpedienteAmarillo, opt => opt.MapFrom(s => s.INDICADOR_EXPEDIENTE_AMARILLO))
                .ForMember(d => d.IndicadorExpedienteRojo, opt => opt.MapFrom(s => s.INDICADOR_EXPEDIENTE_ROJO));
        }
    }
}
