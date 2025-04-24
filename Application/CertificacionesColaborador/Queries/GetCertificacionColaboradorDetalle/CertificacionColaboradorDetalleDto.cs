using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetCertificacionColaboradorDetalle
{
    public class CertificacionColaboradorDetalleDto : IMapFrom<CertificacionColaborador>
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


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CertificacionColaborador, CertificacionColaboradorDetalleDto>()
                .ForMember(d => d.IDCERTIFICACIONCOLABORADOR, opt => opt.MapFrom(s => s.IDCERTIFICACIONCOLABORADOR))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.IDCERTIFICACIONPROVEEDOR, opt => opt.MapFrom(s => s.IDCERTIFICACIONPROVEEDOR))
                .ForMember(d => d.IDCERTIFICACION, opt => opt.MapFrom(s => s.IDCERTIFICACION))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.ANIO, opt => opt.MapFrom(s => s.ANIO))
                .ForMember(d => d.VENCE, opt => opt.MapFrom(s => s.VENCE))
                .ForMember(d => d.Certificacion, opt => opt.MapFrom(s => s.Certificacion.NOMBRE))
                .ForMember(d => d.IdProveedorCertificacion, opt => opt.MapFrom(s => s.CertificacionProveedor.ProveedorCertificacion.IDPROVEEDORCERTIFICACION))
                .ForMember(d => d.ProveedorCertificacion, opt => opt.MapFrom(s => s.CertificacionProveedor.ProveedorCertificacion.Nombre));
        }


    }
}
