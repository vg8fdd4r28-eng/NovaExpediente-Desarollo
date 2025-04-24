using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaConsulta
{
    public class NivelIdiomaConsultaDto : IMapFrom<NivelIdioma>
    {

        public string value { get; set; }
        public string label { get; set; }
        public int key { get; set; }


        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<NivelIdioma, NivelIdiomaConsultaDto>()
                .ForMember(d => d.key, opt => opt.MapFrom(s => s.IDNIVELIDIOMA))
                .ForMember(d => d.value, opt => opt.MapFrom(s => s.Idioma.NOMBRE + " - " + s.NOMBRE))
                .ForMember(d => d.label, opt => opt.MapFrom(s => s.Idioma.NOMBRE + " - " + s.NOMBRE));
        }

       
    }
}

