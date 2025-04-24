using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Especialidades.Queries.GetListaEspecialidadConsulta
{
    public class EspecialidadConsultaDto : IMapFrom<Especialidad>
    {

        public string value { get; set; }
        public string label { get; set; }
        public int key { get; set; }


        public void Mapping(Profile profile)
        {


            profile.CreateMap<Especialidad, EspecialidadConsultaDto>()
                .ForMember(d => d.key, opt => opt.MapFrom(s => s.IDESPECIALIDAD))
                .ForMember(d => d.value, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.label, opt => opt.MapFrom(s => s.NOMBRE));
        }


    }
}

