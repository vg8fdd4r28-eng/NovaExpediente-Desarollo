using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Queries.GetListaSorteos
{
    public class SorteoDto : IMapFrom<Sorteo>
    {
        public int IdSorteo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sorteo, SorteoDto>()
            .ForMember(d => d.IdSorteo, opt => opt.MapFrom(s => s.IDSORTEO))
            .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
            .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION))
            .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO))
            .ForMember(d => d.FechaVencimiento, opt => opt.MapFrom(s => s.FECHAVENCIMIENTO));
        }
    }
}
