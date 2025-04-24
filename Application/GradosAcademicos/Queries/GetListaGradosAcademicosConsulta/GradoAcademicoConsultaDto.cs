using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.GradosAcademicos.Queries.GetListaGradosAcademicosConsulta
{
    public class GradoAcademicoConsultaDto : IMapFrom<GradoAcademico>
    {

        public string value { get; set; }
        public string label { get; set; }
        public int key { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<GradoAcademico, GradoAcademicoConsultaDto>()
                .ForMember(d => d.key, opt => opt.MapFrom(s => s.IdGradoAcademico))
                .ForMember(d => d.value, opt => opt.MapFrom(s => s.Nombre))
                .ForMember(d => d.label, opt => opt.MapFrom(s => s.Nombre));
        }
    }
}
