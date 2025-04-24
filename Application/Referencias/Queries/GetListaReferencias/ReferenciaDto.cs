using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Referencias.Queries.GetListaReferencias
{
    public class ReferenciaDto : IMapFrom<Referencia>
    {

        public int IDREFERENCIA { get; set; }
        public int IDCLIENTE { get; set; }
        public string IDENTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public string EXTENSION { get; set; }
        public string CELULAR { get; set; }
        public string PUESTO { get; set; }
        public bool ACTIVO { get; set; }

        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
      //  public string Cliente { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Referencia, ReferenciaDto>()
                .ForMember(d => d.IDREFERENCIA, opt => opt.MapFrom(s => s.IDREFERENCIA))
                .ForMember(d => d.IDCLIENTE, opt => opt.MapFrom(s => s.IDCLIENTE))
                .ForMember(d => d.IDENTIFICACION, opt => opt.MapFrom(s => s.IDENTIFICACION))
                .ForMember(d => d.CORREO, opt => opt.MapFrom(s => s.CORREO))
                .ForMember(d => d.TELEFONO, opt => opt.MapFrom(s => s.TELEFONO))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.EXTENSION, opt => opt.MapFrom(s => s.EXTENSION))
                .ForMember(d => d.CELULAR, opt => opt.MapFrom(s => s.CELULAR))
                .ForMember(d => d.PUESTO, opt => opt.MapFrom(s => s.PUESTO))
                .ForMember(d => d.ACTIVO, opt => opt.MapFrom(s => s.ACTIVO));
        }


    }
}
