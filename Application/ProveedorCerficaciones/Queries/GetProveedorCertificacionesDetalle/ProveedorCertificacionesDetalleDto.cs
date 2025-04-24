using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;



namespace NovaExpediente.Application.ProveedorCerficaciones.Queries.GetProveedorCertificacionesDetalle
{
    public class ProveedorCertificacionesDetalleDto : IMapFrom<ProveedorCertificacion>
    {
        public int IdProveedorCertificacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<ProveedorCertificacion, ProveedorCertificacionesDetalleDto>()

                .ForMember(d => d.IdProveedorCertificacion, opt => opt.MapFrom(s => s.IDPROVEEDORCERTIFICACION))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.Descripcion));
                
        }
    }
}
