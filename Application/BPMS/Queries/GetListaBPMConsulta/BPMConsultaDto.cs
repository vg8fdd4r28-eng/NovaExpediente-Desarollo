using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.BPMS.Queries.GetListaBPMConsulta
{
    public class BPMConsultaDto : IMapFrom<BPM>
    {

        public string value { get; set; }
        public string label { get; set; }
        public int key { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<BPM, BPMConsultaDto>()
               .ForMember(d => d.key, opt => opt.MapFrom(s => s.IDBPM))
               .ForMember(d => d.value, opt => opt.MapFrom(s => s.Nombre))
               .ForMember(d => d.label, opt => opt.MapFrom(s => s.Nombre));
        }


    }
}

