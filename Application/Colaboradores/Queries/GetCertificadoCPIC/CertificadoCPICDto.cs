using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetCertificadoCPIC
{
    public class CertificadoCPICDto : IMapFrom<Colaborador>
    {
        public string Certificado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Colaborador, CertificadoCPICDto>()
                .ForMember(d => d.Certificado, opt => opt.MapFrom(s => s.CERTIFICADOCPIC != null ? 
                                                                               string.Concat(s.MIMETYPECPIC, Convert.ToBase64String(s.CERTIFICADOCPIC)) : ""));
        }
    }
}
