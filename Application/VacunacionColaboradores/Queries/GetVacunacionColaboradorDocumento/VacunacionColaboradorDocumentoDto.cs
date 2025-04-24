using System;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorDocumento
{
    public class VacunacionColaboradorDocumentoDto : IMapFrom<VacunacionColaborador>
    {


        public string CERTIFICADO { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<VacunacionColaborador, VacunacionColaboradorDocumentoDto>()
                .ForMember(d => d.CERTIFICADO, opt => opt.MapFrom(s => s.Certificado != null ?
                                                                               string.Concat(s.MimeType, Convert.ToBase64String(s.Certificado)) : ""));
        }


    }
}
