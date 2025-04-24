
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetListaVacunacionColaborador
{
    public class VacunacionColaboradorDto : IMapFrom<VacunacionColaborador>
    {

        [Key]
        public int IdVacunacionColaborador { get; set; }

        public int IdUsuarioCrea { get; set; }

        public int? IdUsuarioModifica { get; set; }

        public int IdColaborador { get; set; }

        public string Certificado { get; set; }

        public char Estado { get; set; }

        public char? TipoDocumento { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
        List<VacunasColaborador> VacunasColaborador { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<VacunacionColaborador, VacunacionColaboradorDto>()

            .ForMember(d => d.IdVacunacionColaborador, opt => opt.MapFrom(s => s.IdVacunacionColaborador))
            .ForMember(d => d.IdUsuarioCrea, opt => opt.MapFrom(s => s.IDUSUARIOCREA))
            .ForMember(d => d.IdUsuarioModifica, opt => opt.MapFrom(s => s.IDUSUARIOMODIFICA))
            .ForMember(d => d.IdColaborador, opt => opt.MapFrom(s => s.IdColaborador))
            //.ForMember(d => d.Certificado, opt => opt.MapFrom(s => s.Certificado))
                            .ForMember(d => d.Certificado, opt => opt.MapFrom(s => s.Certificado != null ?
                                                                               string.Concat(s.MimeType, Convert.ToBase64String(s.Certificado)) : ""))
           // .ForMember(d => d.MimeType, opt => opt.MapFrom(s => s.MimeType))
            .ForMember(d => d.Estado, opt => opt.MapFrom(s => s.Estado))
            .ForMember(d => d.TipoDocumento, opt => opt.MapFrom(s => s.TipoDocumento))
            .ForMember(d => d.FechaCreacion, opt => opt.MapFrom(s => s.FECHACREACION))
            .ForMember(d => d.FechaModificacion, opt => opt.MapFrom(s => s.FECHAMODIFICACION))
            .ForMember(d => d.VacunasColaborador, opt => opt.MapFrom(s => s.VacunasColaborador));
        }
    }
}