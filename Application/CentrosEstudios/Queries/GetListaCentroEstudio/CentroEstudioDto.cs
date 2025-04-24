using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.CentrosEstudios.Queries.GetListaCentroEstudio
{
    public class CentroEstudioDto : IMapFrom<CentroEstudio>
    {

        public int IdCentroEstudio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<CentroEstudio, CentroEstudioDto>()
                .ForMember(d => d.IdCentroEstudio, opt => opt.MapFrom(s => s.IDCENTROESTUDIO))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.Descripcion));
        }
    }
}
