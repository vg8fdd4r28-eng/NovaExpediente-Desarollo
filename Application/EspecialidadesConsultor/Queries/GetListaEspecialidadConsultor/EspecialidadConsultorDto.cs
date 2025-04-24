using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.EspecialidadesConsultor.Queries.GetListaEspecialidadConsultor
{
    public class EspecialidadConsultorDto : IMapFrom<EspecialidadConsultor>
    {

        public int IDESPECIALIDADCONSULTOR { get; set; }
        public int IDESPECIALIDAD { get; set; }
        public int IDUSUARIOCREA { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public int EXPERIENCIA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string Especialidad { get; set; }
        public string NivelConocimiento { get; set; }
        public decimal PorcentajeConocimiento { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EspecialidadConsultor, EspecialidadConsultorDto>()
                .ForMember(d => d.IDESPECIALIDADCONSULTOR, opt => opt.MapFrom(s => s.IDESPECIALIDADCONSULTOR))
                .ForMember(d => d.Especialidad, opt => opt.MapFrom(s => s.Especialidad.NOMBRE))
                .ForMember(d => d.NivelConocimiento, opt => opt.MapFrom(s => s.NivelConocimiento.NOMBRE.Substring(3,100)))
                .ForMember(d => d.PorcentajeConocimiento, opt => opt.MapFrom(s => Convert.ToDecimal(s.NivelConocimiento.NOMBRE.Substring(0, 1))/5));
        }


    }
}
