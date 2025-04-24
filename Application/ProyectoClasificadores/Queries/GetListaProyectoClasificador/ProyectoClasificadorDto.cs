using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ProyectoClasificadores.Queries.GetListaProyectoClasificador
{
    public class ProyectoClasificadorDto : IMapFrom<ProyectoClasificador>
    {

        public int IDPROYECTO { get; set; }
        public int IDCLASIFICADOR { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        public string Clasificador { get; set; }
     


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProyectoClasificador, ProyectoClasificadorDto>()
                .ForMember(d => d.IDPROYECTO, opt => opt.MapFrom(s => s.IDPROYECTO))
                .ForMember(d => d.IDCLASIFICADOR, opt => opt.MapFrom(s => s.IDCLASIFICADOR))
                .ForMember(d => d.Clasificador, opt => opt.MapFrom(s => s.Clasificador.Nombre));
        }


    }
}
