using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetCertificacionDocumento
{
    public class CertificacionDocumentoDto : IMapFrom<CertificacionColaborador>
    {
        public string Certificado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CertificacionColaborador, CertificacionDocumentoDto>()
                .ForMember(d => d.Certificado, opt => opt.MapFrom(s => s.CERTIFICADO != null ? 
                                                                               string.Concat(s.MIMETYPE, Convert.ToBase64String(s.CERTIFICADO)) : ""));
        }
    }
}
