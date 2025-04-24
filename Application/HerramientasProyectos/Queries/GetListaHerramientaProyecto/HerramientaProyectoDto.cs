using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.HerramientasProyectos.Queries.GetListaHerramientaProyecto
{
    public class HerramientaProyectoDto : IMapFrom<HerramientaProyecto>
    {

        public int IDHERRAMIENTAPROYECTO { get; set; }
        public int IDEXPERIENCIAPROYECTO { get; set; }
        public string NOMBRE { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }




        public void Mapping(Profile profile)
        {
            profile.CreateMap<HerramientaProyecto, HerramientaProyectoDto>()
                .ForMember(d => d.IDHERRAMIENTAPROYECTO, opt => opt.MapFrom(s => s.IDHERRAMIENTAPROYECTO))
                .ForMember(d => d.IDEXPERIENCIAPROYECTO, opt => opt.MapFrom(s => s.IDEXPERIENCIAPROYECTO))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE));
        }


    }
}
