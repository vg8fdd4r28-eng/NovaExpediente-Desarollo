using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.CertificacionesProveedor.Queries.GetListaProveedorXCertificacion
{
    public class CertificacionProveedorXCertificacionDto : IMapFrom<CertificacionProveedor>
    {

        public int IDCERTIFICACIONPROVEEDOR { get; set; }
        public int IDPROVEEDORCERTIFICACION { get; set; }
        public int IDCERTIFICACION { get; set; }
        public DateTime? VENCE { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
     //  public string Certificacion { get; set; }
        public string ProveedorCertificacion { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CertificacionProveedor, CertificacionProveedorXCertificacionDto>()
                .ForMember(d => d.IDCERTIFICACIONPROVEEDOR, opt => opt.MapFrom(s => s.IDCERTIFICACIONPROVEEDOR))
                .ForMember(d => d.IDPROVEEDORCERTIFICACION, opt => opt.MapFrom(s => s.IDPROVEEDORCERTIFICACION))
                .ForMember(d => d.IDCERTIFICACION, opt => opt.MapFrom(s => s.IDCERTIFICACION))
              //  .ForMember(d => d.Certificacion, opt => opt.MapFrom(s => s.Certificacion.NOMBRE))
                .ForMember(d => d.ProveedorCertificacion, opt => opt.MapFrom(s => s.ProveedorCertificacion.Nombre));
        }


    }
}
