using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Infraestructuras.Queries.GetListaInfraestructura
{
    public class InfraestructuraDto : IMapFrom<Infraestructura>
    {
        public int IDINFRAESTRUCTURA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }

        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }


        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<Infraestructura, InfraestructuraDto>()
                .ForMember(d => d.IDINFRAESTRUCTURA, opt => opt.MapFrom(s => s.IDINFRAESTRUCTURA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }

       
    }
}

