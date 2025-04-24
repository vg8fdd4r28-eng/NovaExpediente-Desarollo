using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Buses.Queries.GetListaBusConsulta
{
    public class BusConsultaDto : IMapFrom<Bus>
    {
        public int idBus { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bus, BusConsultaDto>()
               .ForMember(d => d.idBus, opt => opt.MapFrom(s => s.IdBus))
               .ForMember(d => d.nombre, opt => opt.MapFrom(s => s.Nombre))
               .ForMember(d => d.descripcion, opt => opt.MapFrom(s => s.Descripcion));
        }


    }
}

