using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Vacaciones.Queries.GetListaVacaciones
{
    public class VacacionDto : IMapFrom<Vacacion>
    {

        public int IDVACACION { get; set; }
        public int IDCOLABORADOR { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public int CANTIDADDIAS { get; set; }
        public string colaboradorNombre { get; set; }
        public string identificacionColaborador { get; set; }
        public int? IdSolicitud { get; set; }
        public string Estado { get; set; }
        public int? idNomina { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<Vacacion, VacacionDto>()
                .ForMember(d => d.IDVACACION, opt => opt.MapFrom(s => s.IDVACACION))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.FECHAINICIO, opt => opt.MapFrom(s => s.FECHAINICIO))
                .ForMember(d => d.FECHAFINAL, opt => opt.MapFrom(s => s.FECHAFINAL))
                .ForMember(d => d.CANTIDADDIAS, opt => opt.MapFrom(s => s.CANTIDADDIAS))
                .ForMember(d => d.IdSolicitud, opt => opt.MapFrom(s => s.IDSOLICITUDMASTERDATA))
                .ForMember(d => d.identificacionColaborador, opt => opt.MapFrom(s => s.Colaborador.IDENTIFICACION))
                .ForMember(d => d.colaboradorNombre, opt => opt.MapFrom(s => s.Colaborador.NOMBRE + " " + s.Colaborador.PRIMERAPELLIDO + " " + s.Colaborador.SEGUNDOAPELLIDO ))
                .ForMember(d => d.idNomina, opt => opt.MapFrom(s => s.Colaborador.IDNOMINA ))
;
        }


    }
}
