using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresAsignadosDM
{
    public class ColaboradoresAsignadosDMDto : IMapFrom<Colaborador>
    {

        public string IDENTIFICACION { get; set; }
        public string NombreCompleto { get; set; }
        public string Status { get; set; }
        public string TipoSolicitud { get; set; }
        public DateTime UltimaFecha { get; set; }

        


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Colaborador, ColaboradoresAsignadosDMDto>()
                .ForMember(d => d.IDENTIFICACION, opt => opt.MapFrom(s => s.IDENTIFICACION))
                .ForMember(d => d.NombreCompleto, opt => opt.MapFrom(s => s.NOMBRE + " " + s.PRIMERAPELLIDO + " " + s.SEGUNDOAPELLIDO));


        }


    }
}
