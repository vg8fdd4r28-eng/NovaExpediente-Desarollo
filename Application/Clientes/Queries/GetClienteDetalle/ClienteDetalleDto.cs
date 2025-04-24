using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Clientes.Queries.GetClienteDetalle
{
    public class ClienteDetalleDto : IMapFrom<Cliente>
    {

        public int IDCLIENTE { get; set; }
        public int IDSECTOR { get; set; }
        public int IDPAIS { get; set; }
        public string CODIGOCLIENTE { get; set; }
        public string CEDULAJURIDICA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public bool ACTIVO { get; set; }
        public string TIPOCLIENTE { get; set; }


        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }
        public bool ESREFERENCIA { get; set; }




        public void Mapping(Profile profile)
        {


            profile.CreateMap<Cliente, ClienteDetalleDto>()
                .ForMember(d => d.IDCLIENTE, opt => opt.MapFrom(s => s.IDCLIENTE))
                .ForMember(d => d.IDSECTOR, opt => opt.MapFrom(s => s.IDSECTOR))
                .ForMember(d => d.IDPAIS, opt => opt.MapFrom(s => s.IDPAIS))
                .ForMember(d => d.CODIGOCLIENTE, opt => opt.MapFrom(s => s.CODIGOCLIENTE))
                .ForMember(d => d.CEDULAJURIDICA, opt => opt.MapFrom(s => s.CEDULAJURIDICA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.CORREO, opt => opt.MapFrom(s => s.CORREO))
                .ForMember(d => d.TELEFONO, opt => opt.MapFrom(s => s.TELEFONO))
                .ForMember(d => d.ACTIVO, opt => opt.MapFrom(s => s.ACTIVO))
               .ForMember(d => d.ESREFERENCIA, opt => opt.MapFrom(s => s.ESREFERENCIA))
                .ForMember(d => d.TIPOCLIENTE, opt => opt.MapFrom(s => s.TIPOCLIENTE));
        }


    }
}

