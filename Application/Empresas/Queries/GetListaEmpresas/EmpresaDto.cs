using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Empresas.Queries.GetListaEmpresas
{
    public class EmpresaDto : IMapFrom<Empresa>
    {

        public int IDEMPRESA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string CEDULAJURIDICA { get; set; }

        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public DateTime INICIOVERSION { get; set; }
        public DateTime FINVERSION { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Empresa, EmpresaDto>()
                .ForMember(d => d.IDEMPRESA, opt => opt.MapFrom(s => s.IDEMPRESA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.CEDULAJURIDICA, opt => opt.MapFrom(s => s.CEDULAJURIDICA))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }


    }
}
