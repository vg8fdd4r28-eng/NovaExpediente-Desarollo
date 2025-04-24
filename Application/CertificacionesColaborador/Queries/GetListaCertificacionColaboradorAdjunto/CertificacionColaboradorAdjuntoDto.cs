using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaboradorAdjunto
{
    public class CertificacionColaboradorAdjuntoDto : IMapFrom<CertificacionColaborador>
    {

        public int IDCERTIFICACIONCOLABORADOR { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDCERTIFICACIONPROVEEDOR { get; set; }
        public int IDCERTIFICACION { get; set; }
        public string DESCRIPCION { get; set; }
        public int ANIO { get; set; }
        public DateTime? VENCE { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string Certificacion { get; set; }
        public string ProveedorCertificacion { get; set; }
        public int IdProveedorCertificacion { get; set; }
        public string CERTIFICADO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CertificacionColaborador, CertificacionColaboradorAdjuntoDto>()
                .ForMember(d => d.IDCERTIFICACIONCOLABORADOR, opt => opt.MapFrom(s => s.IDCERTIFICACIONCOLABORADOR))
                .ForMember(d => d.Certificacion, opt => opt.MapFrom(s => s.Certificacion.NOMBRE))
                .ForMember(d => d.CERTIFICADO, opt => opt.MapFrom(s => s.CERTIFICADO != null ? $"{s.MIMETYPE}{Convert.ToBase64String(s.CERTIFICADO)}" : null))
                .ForMember(d => d.IdProveedorCertificacion, opt => opt.MapFrom(s => s.CertificacionProveedor.ProveedorCertificacion.IDPROVEEDORCERTIFICACION))
                .ForMember(d => d.ProveedorCertificacion, opt => opt.MapFrom(s => s.CertificacionProveedor.ProveedorCertificacion.Nombre));
        }


    }
}
