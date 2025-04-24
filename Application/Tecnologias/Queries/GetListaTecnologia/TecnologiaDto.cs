using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Tecnologias.Queries.GetListaTecnologia
{
    public class TecnologiaDto : IMapFrom<Tecnologia>
    {

        public int IDTECNOLOGIA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string VERSIONES { get; set; }


        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }


  
        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<Tecnologia, TecnologiaDto>()
                .ForMember(d => d.IDTECNOLOGIA, opt => opt.MapFrom(s => s.IDTECNOLOGIA))
                .ForMember(d => d.VERSIONES, opt => opt.MapFrom(s => s.VERSIONES))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }

       
    }
}

