using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ErpCores.Queries.GetListaErpCoreConsulta
{
    public class ErpCoreConsultaDto : IMapFrom<ErpCore>
    {

        public string value { get; set; }
        public string label { get; set; }
        public int key { get; set; }


        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<ErpCore, ErpCoreConsultaDto>()
                .ForMember(d => d.key, opt => opt.MapFrom(s => s.IDERPCORE))
                .ForMember(d => d.value, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.label, opt => opt.MapFrom(s => s.NOMBRE));
        }

       
    }
}

