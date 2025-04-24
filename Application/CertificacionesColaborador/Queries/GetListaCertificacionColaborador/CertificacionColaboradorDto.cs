using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaborador
{
    public class CertificacionColaboradorDto : IMapFrom<CertificacionColaborador>
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
        public bool TieneCertificado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CertificacionColaborador, CertificacionColaboradorDto>()
                .ForMember(d => d.IDCERTIFICACIONCOLABORADOR, opt => opt.MapFrom(s => s.IDCERTIFICACIONCOLABORADOR))
                .ForMember(d => d.Certificacion, opt => opt.MapFrom(s => s.Certificacion.NOMBRE))
                .ForMember(d => d.TieneCertificado, opt => opt.MapFrom(s => s.CERTIFICADO != null))
                .ForMember(d => d.ProveedorCertificacion, opt => opt.MapFrom(s => s.CertificacionProveedor.ProveedorCertificacion.Nombre));
        }


    }
}
