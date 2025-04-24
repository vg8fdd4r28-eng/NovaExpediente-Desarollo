using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaExperienciaProyecto;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetExperienciaProyectoDetalle
{
    public class ExperienciaProyectoDetalleDto : IMapFrom<ExperienciaProyecto>
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

        public List<ExperienciaProyectoContactoDto> Contactos { get; set; }

        // public string Colaborador { get; set; }
        // public string TecnologiaProyecto { get; set; }
        // public string HerramientaProyecto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExperienciaProyecto, ExperienciaProyectoDetalleDto>()
                .ForMember(d => d.IDEXPERIENCIAPROYECTO, opt => opt.MapFrom(s => s.IDEXPERIENCIAPROYECTO))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.NOMBREPROYECTO, opt => opt.MapFrom(s => s.NOMBREPROYECTO))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.NOMBRECLIENTE, opt => opt.MapFrom(s => s.NOMBRECLIENTE))
                .ForMember(d => d.ROLPROYECTO, opt => opt.MapFrom(s => s.ROLPROYECTO))

                .ForMember(d => d.LICITACION, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.LICITACION) ? "No registrada " : s.LICITACION))
                // .ForMember(i => i.TecnologiaProyecto, opt => opt.MapFrom(i => i.TecnologiaProyecto.Count ==0 ? "Sin Datos" : string.Join(", ", i.TecnologiaProyecto.Select(a => a.NOMBRE).ToArray())))
                // .ForMember(i => i.HerramientaProyecto, opt => opt.MapFrom(i => i.HerramientaProyecto.Count == 0 ? "Sin Datos" : string.Join(", ", i.HerramientaProyecto.Select(a => a.NOMBRE).ToArray())))
                // .ForMember(d => d.Colaborador, opt => opt.MapFrom(s => s.Colaborador.NOMBRE ))
                .ForMember(d => d.Contactos, opt => opt.MapFrom(s => s.Contactos));

        }


    }
}
