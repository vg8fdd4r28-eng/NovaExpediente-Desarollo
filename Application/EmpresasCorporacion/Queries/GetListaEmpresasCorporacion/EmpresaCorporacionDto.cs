using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.EmpresasCorporacion.Queries.GetListaEmpresasCorporacion
{
    public class EmpresaCorporacionDto : IMapFrom<EmpresaCorporacion>
    {

        public int IDEMPRESACORPORACION { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public DateTime INICIOVERSION { get; set; }
        public DateTime FINVERSION { get; set; }
        public string valor { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmpresaCorporacion, EmpresaCorporacionDto>()
                .ForMember(d => d.IDEMPRESACORPORACION, opt => opt.MapFrom(s => s.IDEMPRESACORPORACION))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.valor, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }


    }
}
