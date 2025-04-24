using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.BPMS.Queries.GetListaBPM
{
    public class BPMDto : IMapFrom<BPM>
    {
        public int IdBPM { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<BPM, BPMDto>()
                .ForMember(d => d.IdBPM, opt => opt.MapFrom(s => s.IDBPM))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.Descripcion));
        }
    }
}
