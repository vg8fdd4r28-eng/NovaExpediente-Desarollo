using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ExperienciasERP.Queries.GetListaExperienciasERP
{
    public class ExperienciaERPDto : IMapFrom<ExperienciaERP>
    {
        public int IDCOLABORADOR { get; set; }
        public int IDERPCORE { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }
        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public int IDTIPOCORE { get; set; }
        public string TECNOLOGIA { get; set; }

        public bool ESOPENSOURCE { get; set; }
        public string ErpCore { get; set; }
        public string TipoCore { get; set; }
        public string NivelConocimiento { get; set; }
        public decimal PorcentajeConocimiento { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExperienciaERP, ExperienciaERPDto>()
                .ForMember(d => d.IDERPCORE, opt => opt.MapFrom(s => s.IDERPCORE))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.IDNIVELCONOCIMIENTO, opt => opt.MapFrom(s => s.IDNIVELCONOCIMIENTO))
                .ForMember(d => d.MESESEXPERIENCIA, opt => opt.MapFrom(s => s.MESESEXPERIENCIA))
                .ForMember(d => d.IDTIPOCORE, opt => opt.MapFrom(s => s.ErpCore.IDTIPOCORE))
                .ForMember(d => d.TECNOLOGIA, opt => opt.MapFrom(s => s.ErpCore.TECNOLOGIA))
                .ForMember(d => d.ESOPENSOURCE, opt => opt.MapFrom(s => s.ErpCore.ESOPENSOURCE))
                .ForMember(d => d.ErpCore, opt => opt.MapFrom(s => s.ErpCore.NOMBRE))
                .ForMember(d => d.TipoCore, opt => opt.MapFrom(s => s.ErpCore.TipoCore.NOMBRE))
                .ForMember(d => d.NivelConocimiento, opt => opt.MapFrom(s => s.NivelConocimiento.NOMBRE.Substring(3, 100)))
                .ForMember(d => d.PorcentajeConocimiento, opt => opt.MapFrom(s => Convert.ToDecimal(s.NivelConocimiento.NOMBRE.Substring(0, 1)) / 5));
        }


    }
}
