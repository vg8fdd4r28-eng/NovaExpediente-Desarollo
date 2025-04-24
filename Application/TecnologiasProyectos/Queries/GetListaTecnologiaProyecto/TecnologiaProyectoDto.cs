using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.TecnologiasProyectos.Queries.GetListaTecnologiaProyecto
{
    public class TecnologiaProyectoDto : IMapFrom<TecnologiaProyecto>
    {

        public int IDTECNOLOGIAPROYECTO { get; set; }
        public int IDEXPERIENCIAPROYECTO { get; set; }
        public string NOMBRE { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        public string AmbienteDesarrollo { get; set; }
     


        public void Mapping(Profile profile)
        {
            profile.CreateMap<TecnologiaProyecto, TecnologiaProyectoDto>()
                .ForMember(d => d.IDTECNOLOGIAPROYECTO, opt => opt.MapFrom(s => s.IDTECNOLOGIAPROYECTO))
                .ForMember(d => d.IDEXPERIENCIAPROYECTO, opt => opt.MapFrom(s => s.IDEXPERIENCIAPROYECTO))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE));
        }


    }
}
