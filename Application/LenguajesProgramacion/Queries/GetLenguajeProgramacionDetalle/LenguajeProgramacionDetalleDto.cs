using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.LenguajesProgramacion.Queries.GetLenguajeProgramacionDetalle
{
    public class LenguajeProgramacionDetalleDto : IMapFrom<LenguajeProgramacion>
    {

        public int IDLENGUAJEPROGRAMACION { get; set; }
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
            
 
            profile.CreateMap<LenguajeProgramacion, LenguajeProgramacionDetalleDto>()
                .ForMember(d => d.IDLENGUAJEPROGRAMACION, opt => opt.MapFrom(s => s.IDLENGUAJEPROGRAMACION))
                .ForMember(d => d.VERSIONES, opt => opt.MapFrom(s => s.VERSIONES))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }

       
    }
}

