using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System.Linq;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaExperienciaProyecto
{
    public class ExperienciaProyectoDto : IMapFrom<ExperienciaProyecto>
    {

        public int IDEXPERIENCIAPROYECTO { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBREPROYECTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBRECLIENTE { get; set; }
        public DateTime? FECHAINICIO { get; set; }
        public DateTime? FECHAFINAL { get; set; }
        public string INICIO { get; set; }
        public string FIN { get; set; }
        public string ROLPROYECTO { get; set; }
        public string LICITACION { get; set; }


        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        // public string Colaborador { get; set; }
        public string TecnologiaProyecto { get; set; }

        public string HerramientaProyecto { get; set; }
        public int? IDEMPRESACORPORACION { get; set; }
        public int? IDCLIENTE { get; set; }
        public string NOMBREEMPRESACORPORACION { get; set; }
        public string NOMBREEMPRESA { get; set; }
        public string NOMBRECLIENTEEMPRESACORPORACION { get; set; }
        public List<ExperienciaProyectoContactoDto> Contactos { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExperienciaProyecto, ExperienciaProyectoDto>()
                .ForMember(d => d.IDEXPERIENCIAPROYECTO, opt => opt.MapFrom(s => s.IDEXPERIENCIAPROYECTO))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.NOMBREPROYECTO, opt => opt.MapFrom(s => s.NOMBREPROYECTO))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.NOMBRECLIENTE, opt => opt.MapFrom(s => s.NOMBRECLIENTE))
                .ForMember(d => d.ROLPROYECTO, opt => opt.MapFrom(s => s.ROLPROYECTO))
                .ForMember(d => d.NOMBREEMPRESA, opt => opt.MapFrom(s => s.NOMBREEMPRESA))
                .ForMember(d => d.NOMBREEMPRESACORPORACION, opt => opt.MapFrom(s => s.EmpresaCorporacion.NOMBRE))
                .ForMember(d => d.NOMBRECLIENTEEMPRESACORPORACION, opt => opt.MapFrom(s => s.Cliente.NOMBRE))
                .ForMember(d => d.IDEMPRESACORPORACION, opt => opt.MapFrom(s => s.IDEMPRESACORPORACION))
                .ForMember(d => d.LICITACION, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.LICITACION) ? "No registrada " : s.LICITACION))
                .ForMember(i => i.TecnologiaProyecto, opt => opt.MapFrom(i => i.TecnologiaProyecto.Count ==0 ? "Sin Datos" : string.Join(", ", i.TecnologiaProyecto.Select(a => a.NOMBRE).ToArray())))
                .ForMember(i => i.HerramientaProyecto, opt => opt.MapFrom(i => i.HerramientaProyecto.Count == 0 ? "Sin Datos" : string.Join(", ", i.HerramientaProyecto.Select(a => a.NOMBRE).ToArray())))
                .ForMember(d => d.Contactos, opt => opt.MapFrom(s => s.Contactos));
            //    .ForMember(d => d.Colaborador, opt => opt.MapFrom(s => s.Colaborador.NOMBRE ));
        }


    }
}
