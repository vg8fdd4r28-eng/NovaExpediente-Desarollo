using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.CurriculumVitae.Queries.GetCurriculumVitae
{
    public class CurriculumVitaeDto : IMapFrom<Domain.Entities.CurriculumVitae>
    {
        public int IDCURRICULUMVITAE { get; set; }
        public string NOMBREARCHIVO { get; set; }
        //public string CONTENIDOARCHIVO { get; set; }
        public DateTime FECHA { get; set; }
        public string MENSAJE { get; set; }
        public string ESTADO { get; set; }
        public string UUID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.CurriculumVitae, CurriculumVitaeDto>()
            .ForMember(d => d.IDCURRICULUMVITAE, opt => opt.MapFrom(s => s.IDCURRICULUMVITAE))
            .ForMember(d => d.NOMBREARCHIVO, opt => opt.MapFrom(s => s.NOMBREDOCUMENTO))
            //.ForMember(d => d.CONTENIDOARCHIVO, opt => opt.MapFrom(s => s.DOCUMENTO))
            .ForMember(d => d.FECHA, opt => opt.MapFrom(s => s.FECHACREACION))
            .ForMember(d => d.MENSAJE, opt => opt.MapFrom(s => s.MENSAJE))
            .ForMember(d => d.ESTADO, opt => opt.MapFrom(s => s.ESTADO))
            .ForMember(d => d.UUID, opt => opt.MapFrom(s => s.UUID));
        }
    }
}
