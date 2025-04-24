using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ProyectoTecnologias.Queries.GetListaProyectoTecnologia
{
    public class ProyectoTecnologiaDto : IMapFrom<ProyectoTecnologia>
    {

        public int IDPROYECTOTECNOLOGIA { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDTECNOLOGIA { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        public string Tecnologia { get; set; }
     


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProyectoTecnologia, ProyectoTecnologiaDto>()
                .ForMember(d => d.IDPROYECTOTECNOLOGIA, opt => opt.MapFrom(s => s.IDPROYECTOTECNOLOGIA))
                .ForMember(d => d.IDPROYECTO, opt => opt.MapFrom(s => s.IDPROYECTO))
                .ForMember(d => d.IDTECNOLOGIA, opt => opt.MapFrom(s => s.IDTECNOLOGIA))
                .ForMember(d => d.Tecnologia, opt => opt.MapFrom(s => s.Tecnologia.NOMBRE));
        }


    }
}
