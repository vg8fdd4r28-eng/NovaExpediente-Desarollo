using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.GradosAcademicos.Queries.GetListaGradosAcademicos
{
    public class GradoAcademicoDto : IMapFrom<GradoAcademico>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Orden { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GradoAcademico, GradoAcademicoDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IdGradoAcademico))
                          .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre));
        }
    }
}
