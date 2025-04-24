using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Common;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Afiliacion.Queries.GetCertificado
{
    public class CertificadoAfiliacionDto : IMapFrom<AfiliacionColaborador>
    {

        public string Certificado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AfiliacionColaborador, CertificadoAfiliacionDto>()
                .ForMember(d => d.Certificado, opt => opt.MapFrom(s => s.CERTIFICADO != null ? 
                                                                                     string.Concat(s.MIMETYPE, Convert.ToBase64String(s.CERTIFICADO)) : null));
        }


    }
}
