using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidad
{
    public class CalidadDto : IMapFrom<Calidad>
    {

        public int IDCALIDAD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }

        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }




        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<Calidad, CalidadDto>()
                .ForMember(d => d.IDCALIDAD, opt => opt.MapFrom(s => s.IDCALIDAD))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))

                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }

       
    }
}

