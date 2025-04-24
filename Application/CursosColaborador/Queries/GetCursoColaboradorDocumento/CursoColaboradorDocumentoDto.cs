using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetCursoColaboradorDocumento
{
    public class CursoColaboradorDocumentoDto : IMapFrom<CursoColaborador>
    {
        public string Certificado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CursoColaborador, CursoColaboradorDocumentoDto>()
                .ForMember(d => d.Certificado, opt => opt.MapFrom(s => s.CERTIFICADO != null ? 
                                                                               string.Concat(s.MIMETYPE, Convert.ToBase64String(s.CERTIFICADO)) : ""));
        }
    }
}
