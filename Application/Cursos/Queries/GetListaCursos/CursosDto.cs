using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.Cursos.Queries.GetListaCursos
{
    public class CursosDto : IMapFrom<Curso>
    {
        public int IdCurso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<Curso, CursosDto>()
                .ForMember(d => d.IdCurso, opt => opt.MapFrom(s => s.IDCURSO))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION));
        }

    }
}
