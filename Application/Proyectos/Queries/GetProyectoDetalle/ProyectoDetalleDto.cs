using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Proyectos.Queries.GetProyectoDetalle
{
    public class ProyectoDetalleDto : IMapFrom<Proyecto>
    {

        public int IDPROYECTO { get; set; }
        public int IDCLIENTE { get; set; }
        public int IDEMPRESA { get; set; }
        public int IDSECTOR { get; set; }
        public int IDREFERENCIA { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string CODPROYECTO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string NUMEROCONTRATO { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public int CANTIDADHORAS { get; set; }
        public bool ACTIVO { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string Cliente { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Proyecto, ProyectoDetalleDto>()
                .ForMember(d => d.IDPROYECTO, opt => opt.MapFrom(s => s.IDPROYECTO))
                .ForMember(d => d.IDCLIENTE, opt => opt.MapFrom(s => s.IDCLIENTE))
                .ForMember(d => d.IDSECTOR, opt => opt.MapFrom(s => s.IDSECTOR))
                .ForMember(d => d.IDREFERENCIA, opt => opt.MapFrom(s => s.IDREFERENCIA))
                .ForMember(d => d.CODPROYECTO, opt => opt.MapFrom(s => s.CODPROYECTO))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.NUMEROCONTRATO, opt => opt.MapFrom(s => s.NUMEROCONTRATO))
                .ForMember(d => d.FECHAINICIO, opt => opt.MapFrom(s => s.FECHAINICIO))
                .ForMember(d => d.FECHAFINAL, opt => opt.MapFrom(s => s.FECHAFINAL))
                .ForMember(d => d.CANTIDADHORAS, opt => opt.MapFrom(s => s.CANTIDADHORAS))
                .ForMember(d => d.Cliente, opt => opt.MapFrom(s => s.Cliente.NOMBRE))
                .ForMember(d => d.ACTIVO, opt => opt.MapFrom(s => s.ACTIVO));
        }


    }
}
