using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Certificaciones.Queries.GetCertificacionDetalle
{
    public class CertificacionDetalleDto : IMapFrom<Certificacion>
    {

        public int IDCERTIFICACION { get; set; }
        public string CODIGOCERTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }




        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<Certificacion, CertificacionDetalleDto>()
                .ForMember(d => d.IDCERTIFICACION, opt => opt.MapFrom(s => s.IDCERTIFICACION))
                .ForMember(d => d.CODIGOCERTIFICACION, opt => opt.MapFrom(s => s.CODIGOCERTIFICACION))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE));
        }

       
    }
}

