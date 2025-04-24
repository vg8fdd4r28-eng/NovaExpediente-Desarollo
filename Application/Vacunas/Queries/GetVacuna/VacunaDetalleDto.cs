
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Vacunas.Queries.GetVacunaDetalle
{
    public class VacunaDetalleDto : IMapFrom<Vacuna>
    {
            public int IdVacuna {get; set;}
            public int IdUsuarioCrea {get; set;}
            public int? IdUsuarioModifica {get; set;}
            public string Nombre {get; set;}
            public DateTime? FechaCreacion {get; set;}
            public DateTime? FechaModificacion {get; set;}
            public void Mapping(Profile profile)
        {


            profile.CreateMap<Vacuna, VacunaDetalleDto>()

            .ForMember(d => d.IdVacuna, opt => opt.MapFrom(s => s.IdVacuna))
            .ForMember(d => d.IdUsuarioCrea, opt => opt.MapFrom(s => s.IDUSUARIOCREA))
            .ForMember(d => d.IdUsuarioModifica, opt => opt.MapFrom(s => s.IDUSUARIOMODIFICA))
            .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre))
            .ForMember(d => d.FechaCreacion, opt => opt.MapFrom(s => s.FECHACREACION))
            .ForMember(d => d.FechaModificacion, opt => opt.MapFrom(s => s.FECHAMODIFICACION));
                
                
        }
    }
}
