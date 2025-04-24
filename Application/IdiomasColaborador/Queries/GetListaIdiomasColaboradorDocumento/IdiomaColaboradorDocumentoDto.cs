using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorDocumento
{
    public class IdiomaColaboradorDocumentoDto : IMapFrom<IdiomaColaborador>
    {

   
        public string CERTIFICADO { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<IdiomaColaborador, IdiomaColaboradorDocumentoDto>()
                .ForMember(d => d.CERTIFICADO, opt => opt.MapFrom(s => s.CERTIFICADO != null ?
                                                                               string.Concat(s.MIMETYPE, Convert.ToBase64String(s.CERTIFICADO)) : ""));
        }


    }
}
