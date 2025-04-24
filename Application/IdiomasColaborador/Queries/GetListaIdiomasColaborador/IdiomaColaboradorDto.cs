using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador
{
    public class IdiomaColaboradorDto : IMapFrom<IdiomaColaborador>
    {

        public int IDIDIOMA { get; set; }
        public int IDNIVELIDIOMA { get; set; }
        public int IDUSUARIOCREA { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string Idioma { get; set; }
        public string NivelIdioma { get; set; }
        public bool TieneCertificado { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<IdiomaColaborador, IdiomaColaboradorDto>()
                .ForMember(d => d.IDIDIOMA, opt => opt.MapFrom(s => s.IDIDIOMA))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.Idioma, opt => opt.MapFrom(s => s.Idioma.NOMBRE))
                          .ForMember(d => d.TieneCertificado, opt => opt.MapFrom(s => s.CERTIFICADO != null))
                .ForMember(d => d.NivelIdioma, opt => opt.MapFrom(s => s.NivelIdioma.NOMBRE));
        }


    }
}
