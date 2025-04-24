using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaborador
{
    public class CursoColaboradorDto : IMapFrom<CursoColaborador>
    {

        public int IDCURSOCOLABORADOR { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBREENTIDAD { get; set; }
        public decimal? CANTIDADHORAS { get; set; }
        public DateTime? FECHAFINAL { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public bool TieneCertificado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CursoColaborador, CursoColaboradorDto>()
                .ForMember(d => d.IDCURSOCOLABORADOR, opt => opt.MapFrom(s => s.IDCURSOCOLABORADOR))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.TieneCertificado, opt => opt.MapFrom(s => s.CERTIFICADO != null))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE));

        }


    }
}
