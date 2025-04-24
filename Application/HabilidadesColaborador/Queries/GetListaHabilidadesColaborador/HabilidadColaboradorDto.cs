using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.HabilidadesColaborador.Queries.GetListaHabilidadesColaborador
{
    public class HabilidadColaboradorDto : IMapFrom<HabilidadColaborador>
    {

        public int IDHABILIDADCOLABORADOR { get; set; }
        public int IDHABILIDADBLANDA { get; set; }
        public int IDCOLABORADOR { get; set; }
        public char NIVELHABILIDAD { get; set; }
        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string HabilidadBlanda { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabilidadColaborador, HabilidadColaboradorDto>()
                .ForMember(d => d.IDHABILIDADCOLABORADOR, opt => opt.MapFrom(s => s.IDHABILIDADCOLABORADOR))
                .ForMember(d => d.IDHABILIDADBLANDA, opt => opt.MapFrom(s => s.IDHABILIDADBLANDA))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.HabilidadBlanda, opt => opt.MapFrom(s => s.HabilidadBlanda.NOMBRE));
        }


    }
}
