using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ErpCores.Queries.GetListaErpCore
{
    public class ErpCoreDto : IMapFrom<ErpCore>
    {

        public int IDERPCORE { get; set; }
        public int IDTIPOCORE { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ESOPENSOURCE { get; set; }
        public string TECNOLOGIA { get; set; }


        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }


        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<ErpCore, ErpCoreDto>()
                .ForMember(d => d.IDERPCORE, opt => opt.MapFrom(s => s.IDERPCORE))
                .ForMember(d => d.IDTIPOCORE, opt => opt.MapFrom(s => s.IDTIPOCORE))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.TECNOLOGIA, opt => opt.MapFrom(s => s.TECNOLOGIA))
                .ForMember(d => d.ESOPENSOURCE, opt => opt.MapFrom(s => s.ESOPENSOURCE));
                
        }

       
    }
}

