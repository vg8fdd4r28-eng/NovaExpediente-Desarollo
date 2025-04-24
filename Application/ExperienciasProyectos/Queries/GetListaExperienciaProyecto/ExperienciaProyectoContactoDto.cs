using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaExperienciaProyecto
{
    public class ExperienciaProyectoContactoDto : IMapFrom<ExperienciaProyectoContacto>
    {

        public int IDEXPERIENCIAPROYECTOCONTACTO { get; set; }
        public int IDEXPERIENCIAPROYECTO { get; set; }
        public string CONTACTO { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExperienciaProyectoContacto, ExperienciaProyectoContactoDto>()
                .ForMember(d => d.IDEXPERIENCIAPROYECTOCONTACTO, opt => opt.MapFrom(s => s.IDEXPERIENCIAPROYECTOCONTACTO))
                .ForMember(d => d.IDEXPERIENCIAPROYECTO, opt => opt.MapFrom(s => s.IDEXPERIENCIAPROYECTO))
                .ForMember(d => d.CONTACTO, opt => opt.MapFrom(s => s.CONTACTO))
                .ForMember(d => d.CORREO, opt => opt.MapFrom(s => s.CORREO))
                .ForMember(d => d.TELEFONO, opt => opt.MapFrom(s => s.TELEFONO));
        }


    }
}
