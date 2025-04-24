using AutoMapper;
using NovaExpediente.Application.CertificacionesProveedor.Queries.GetListaProveedorXCertificacion;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacionProveedores
{
    public class CertificacionProveedoresDto : IMapFrom<Certificacion>
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

        public string valor { get; set; }
        public string proveedores { get; set; }
        public string estado { get; set; }
        public List<CertificacionProveedorXCertificacionDto> ProveedoresList { get; set; }



        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<Certificacion, CertificacionProveedoresDto>()
                .ForMember(d => d.IDCERTIFICACION, opt => opt.MapFrom(s => s.IDCERTIFICACION))
                .ForMember(d => d.CODIGOCERTIFICACION, opt => opt.MapFrom(s => s.CODIGOCERTIFICACION))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.ProveedoresList, opt => opt.MapFrom(s => s.CertificacionProveedores))
                .ForMember(d => d.proveedores, opt => opt.MapFrom(s => string.Join(", ", s.CertificacionProveedores.Select(a => a.ProveedorCertificacion.Nombre == "" ? "Sin Definir" : a.ProveedorCertificacion.Nombre).ToArray()))) // opt => opt.MapFrom(s => s.CertificacionProveedor.ProveedorCertificacion.Nombre))
                .ForMember(d => d.valor, opt => opt.MapFrom(s => s.NOMBRE));
        }

       
    }
}

