using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaXdioma
{
    public class NivelIdiomaXdiomaDto : IMapFrom<NivelIdioma>
    {

        public int IDNIVELIDIOMA { get; set; }
        public int IDIDIOMA { get; set; }
        public string Nombre_Nivel { get; set; }
        public string DESCRIPCION { get; set; }


        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }

 


        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<NivelIdioma, NivelIdiomaXdiomaDto>()
                .ForMember(d => d.IDNIVELIDIOMA, opt => opt.MapFrom(s => s.IDNIVELIDIOMA))
                .ForMember(d => d.IDIDIOMA, opt => opt.MapFrom(s => s.IDIDIOMA))
                .ForMember(d => d.Nombre_Nivel, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }

       
    }
}

