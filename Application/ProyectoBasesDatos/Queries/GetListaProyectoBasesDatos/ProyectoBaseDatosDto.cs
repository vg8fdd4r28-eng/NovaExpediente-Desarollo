using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ProyectoBasesDatos.Queries.GetListaProyectoBaseDatos
{
    public class ProyectoBaseDatosDto : IMapFrom<ProyectoBaseDatos>
    {

        public int IDPROYECTOBASEDATOS { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDBASEDATOS { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        public string BaseDatos { get; set; }
     


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProyectoBaseDatos, ProyectoBaseDatosDto>()
                .ForMember(d => d.IDPROYECTOBASEDATOS, opt => opt.MapFrom(s => s.IDPROYECTOBASEDATOS))
                .ForMember(d => d.IDPROYECTO, opt => opt.MapFrom(s => s.IDPROYECTO))
                .ForMember(d => d.IDBASEDATOS, opt => opt.MapFrom(s => s.IDBASEDATOS))
                .ForMember(d => d.BaseDatos, opt => opt.MapFrom(s => s.BaseDatos.NOMBRE));
        }


    }
}
