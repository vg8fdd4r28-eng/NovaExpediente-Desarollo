using AutoMapper;
using NovaExpediente.Application.CertificacionesProveedor.Queries.GetListaProveedorXCertificacion;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System.Collections.Generic;



namespace NovaExpediente.Application.ProveedorCerficaciones.Queries.GetListaProveedorCertificacionesList
{
    public class ProveedorCertificacionesListaDto : IMapFrom<ProveedorCertificacion>
    {
        public int IdProveedorCertificacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<CertificacionProveedorXCertificacionDto> CertificacionesList { get; set; }


        public void Mapping(Profile profile)
        {


            profile.CreateMap<ProveedorCertificacion, ProveedorCertificacionesListaDto>()

                .ForMember(d => d.IdProveedorCertificacion, opt => opt.MapFrom(s => s.IDPROVEEDORCERTIFICACION))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre))
                .ForMember(d => d.CertificacionesList, opt => opt.MapFrom(s => s.ProveedorCertificaciones))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.Descripcion));
                
        }
    }
}
