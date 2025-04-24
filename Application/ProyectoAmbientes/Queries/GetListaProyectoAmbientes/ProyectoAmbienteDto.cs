using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ProyectoAmbientes.Queries.GetListaProyectoAmbientes
{
    public class ProyectoAmbienteDto : IMapFrom<ProyectoAmbiente>
    {

        public int IDPROYECTOAMBIENTE { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDAMBIENTEDESARROLLO { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        public string AmbienteDesarrollo { get; set; }
     


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProyectoAmbiente, ProyectoAmbienteDto>()
                .ForMember(d => d.IDPROYECTOAMBIENTE, opt => opt.MapFrom(s => s.IDPROYECTOAMBIENTE))
                .ForMember(d => d.IDPROYECTO, opt => opt.MapFrom(s => s.IDPROYECTO))
                .ForMember(d => d.IDAMBIENTEDESARROLLO, opt => opt.MapFrom(s => s.IDAMBIENTEDESARROLLO))
                .ForMember(d => d.AmbienteDesarrollo, opt => opt.MapFrom(s => s.AmbienteDesarrollo.NOMBRE));
        }


    }
}
