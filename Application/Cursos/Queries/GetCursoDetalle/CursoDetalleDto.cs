using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.Cursos.Queries.GetCursoDetalle
{
    public class CursoDetalleDto : IMapFrom<Curso>
    {
        public int IdCurso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<Curso, CursoDetalleDto>()
                .ForMember(d => d.IdCurso, opt => opt.MapFrom(s => s.IDCURSO))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION));
        }

    }
}
