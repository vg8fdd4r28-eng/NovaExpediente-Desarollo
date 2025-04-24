using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetCursoColaboradorDetalle
{
    public class CursoColaboradorDetalleDto : IMapFrom<CursoColaborador>
    {

        public int IDCURSOCOLABORADOR { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBREENTIDAD { get; set; }
        public string CERTIFICADO { get; set; }
        public decimal? CANTIDADHORAS { get; set; }
        public DateTime? FECHAFINAL { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CursoColaborador, CursoColaboradorDetalleDto>()
                .ForMember(d => d.IDCURSOCOLABORADOR, opt => opt.MapFrom(s => s.IDCURSOCOLABORADOR))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.NOMBREENTIDAD, opt => opt.MapFrom(s => s.NOMBREENTIDAD))
                .ForMember(d => d.CANTIDADHORAS, opt => opt.MapFrom(s => s.CANTIDADHORAS))
                .ForMember(d => d.FECHAFINAL, opt => opt.MapFrom(s => s.FECHAFINAL))
                .ForMember(d => d.CERTIFICADO, opt => opt.MapFrom(s => s.CERTIFICADO != null ? $"{s.MIMETYPE}{Convert.ToBase64String(s.CERTIFICADO)}" : null))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE));

        }


    }
}
