using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.CursosImpartir.Queries.GetListaCursosImpartir
{
    public class CursoImpartirDto : IMapFrom<CursoImpartir>
    {

        public int IDCURSOIMPARTIR { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDCURSO { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime FECHAMODIFICACION { get; set; }
        public string Curso { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CursoImpartir, CursoImpartirDto>()
                .ForMember(d => d.IDCURSOIMPARTIR, opt => opt.MapFrom(s => s.IDCURSOIMPARTIR))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.IDCURSO, opt => opt.MapFrom(s => s.IDCURSO))
                .ForMember(d => d.Curso, opt => opt.MapFrom(s => s.Curso.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));

        }


    }
}
