using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ColaboradorProyectos.Queries.GetListaColaboradorProyectos
{
    public class ColaboradorProyectoDto : IMapFrom<ColaboradorProyecto>
    {

        public int IDCOLABORADORPROYECTO { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDPUESTO { get; set; }
        public int IDCOLABORADOR { get; set; }
        public decimal TARIFADOLARES { get; set; }
        public int TOTALHORAS { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        public string Proyecto { get; set; }
        public string Puesto { get; set; }
        public string Colaborador { get; set; }
        public string Cliente { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ColaboradorProyecto, ColaboradorProyectoDto>()
                .ForMember(d => d.IDCOLABORADORPROYECTO, opt => opt.MapFrom(s => s.IDCOLABORADORPROYECTO))
                .ForMember(d => d.IDPROYECTO, opt => opt.MapFrom(s => s.IDPROYECTO))
                .ForMember(d => d.IDPUESTO, opt => opt.MapFrom(s => s.IDPUESTO))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.TARIFADOLARES, opt => opt.MapFrom(s => s.TARIFADOLARES))
                .ForMember(d => d.TOTALHORAS, opt => opt.MapFrom(s => s.TOTALHORAS))
                .ForMember(d => d.Proyecto, opt => opt.MapFrom(s => s.Proyecto.NOMBRE))
                .ForMember(d => d.Puesto, opt => opt.MapFrom(s => s.Puesto.NOMBRE))
                .ForMember(d => d.Cliente, opt => opt.MapFrom(s => s.Proyecto.Cliente.NOMBRE))
                .ForMember(d => d.Colaborador, opt => opt.MapFrom(s => s.Colaborador.NOMBRE + " " + s.Colaborador.PRIMERAPELLIDO + " " + s.Colaborador.SEGUNDOAPELLIDO));
        }


    }
}
