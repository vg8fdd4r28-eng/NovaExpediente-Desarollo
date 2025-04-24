using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Sectores.Queries.GetListaSectores
{
    public class SectorDto : IMapFrom<Sector>
    {
        public int IdSector { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<Sector, SectorDto>()

                .ForMember(d => d.IdSector, opt => opt.MapFrom(s => s.IdSector))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.Descripcion));

        }
    }
}
