using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Especialidades.Queries.GetListaEspecialidad
{
    public class EspecialidadDto : IMapFrom<Especialidad>
    {

        public int IDESPECIALIDAD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }


        public void Mapping(Profile profile)
        {


            profile.CreateMap<Especialidad, EspecialidadDto>()
                .ForMember(d => d.IDESPECIALIDAD, opt => opt.MapFrom(s => s.IDESPECIALIDAD))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE));
        }


    }
}

