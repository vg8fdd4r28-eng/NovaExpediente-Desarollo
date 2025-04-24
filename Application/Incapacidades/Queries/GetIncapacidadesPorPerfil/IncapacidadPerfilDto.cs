using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Incapacidades.Queries.GetIncapacidadesPorPerfil
{
    public class IncapacidadPerfilDto : IMapFrom<Incapacidad>
    {

        public int IDINCAPACIDAD { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int? IDCARGAINCAPACIDADES { get; set; }
        public int? IDSOLICITUDMASTERDATA { get; set; }

        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public string NUMBOLETA { get; set; }
        public int CANTIDADDIAS { get; set; }
        public char? INDTIPOREGISTRO { get; set; }
        public char TIPOINCAPACIDAD { get; set; }

        public string colaboradorNombre { get; set; }
        public string identificacionColaborador { get; set; }
        public string Boleta { get; set; }
        public string Estado { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<Incapacidad, IncapacidadPerfilDto>()
                .ForMember(d => d.IDINCAPACIDAD, opt => opt.MapFrom(s => s.IDINCAPACIDAD))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.FECHAINICIO, opt => opt.MapFrom(s => s.FECHAINICIO))
                .ForMember(d => d.FECHAFINAL, opt => opt.MapFrom(s => s.FECHAFINAL))
                .ForMember(d => d.NUMBOLETA, opt => opt.MapFrom(s => s.NUMBOLETA))
                .ForMember(d => d.CANTIDADDIAS, opt => opt.MapFrom(s => s.CANTIDADDIAS))
                .ForMember(d => d.IDCARGAINCAPACIDADES, opt => opt.MapFrom(s => s.IDCARGAINCAPACIDADES))
                .ForMember(d => d.IDSOLICITUDMASTERDATA, opt => opt.MapFrom(s => s.IDSOLICITUDMASTERDATA))
                .ForMember(d => d.TIPOINCAPACIDAD, opt => opt.MapFrom(s => s.TIPOINCAPACIDAD))
                .ForMember(d => d.INDTIPOREGISTRO, opt => opt.MapFrom(s => s.INDTIPOREGISTRO))
                .ForMember(d => d.CANTIDADDIAS, opt => opt.MapFrom(s => s.CANTIDADDIAS))
                .ForMember(d => d.Boleta, opt => opt.MapFrom(s => s.BOLETA != null ? $"{s.MIMETYPE}{Convert.ToBase64String(s.BOLETA)}" : null))
                .ForMember(d => d.identificacionColaborador, opt => opt.MapFrom(s => s.Colaborador.IDENTIFICACION))
                                .ForMember(d => d.Estado, opt => opt.MapFrom(s => s.SolicitudMasterData.ESTADOSOLICITUD))
                .ForMember(d => d.colaboradorNombre, opt => opt.MapFrom(s => s.Colaborador.NOMBRE + " " + s.Colaborador.PRIMERAPELLIDO + " " + s.Colaborador.SEGUNDOAPELLIDO))
;
        }


    }
}
