
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace NovaExpediente.Application.VacunasColaboradores.Queries.GetListaVacunasColaborador
{
    public class VacunasColaboradorDto : IMapFrom<VacunasColaborador>
    {

                [Key]    
        public int IdVacunasColaborador {get; set;}
                    
        public int IdUsuarioCrea {get; set;}
                    
        public int? IdUsuarioModifica {get; set;}
                    
        public int IdVacunacionColaborador {get; set;}
                    
        public int IdVacuna {get; set;}
                    
        public DateTime Fecha {get; set;}
                    
        public DateTime? FechaCreacion {get; set;}
                    
        public DateTime? FechaModificacion {get; set;}
                
        public void Mapping(Profile profile)
        {


            profile.CreateMap<VacunasColaborador, VacunasColaboradorDto>()

            .ForMember(d => d.IdVacunasColaborador, opt => opt.MapFrom(s => s.IdVacunasColaborador))
            .ForMember(d => d.IdUsuarioCrea, opt => opt.MapFrom(s => s.IDUSUARIOCREA))
            .ForMember(d => d.IdUsuarioModifica, opt => opt.MapFrom(s => s.IDUSUARIOMODIFICA))
            .ForMember(d => d.IdVacunacionColaborador, opt => opt.MapFrom(s => s.IdVacunacionColaborador))
            .ForMember(d => d.IdVacuna, opt => opt.MapFrom(s => s.IdVacuna))
            .ForMember(d => d.Fecha, opt => opt.MapFrom(s => s.Fecha))
            .ForMember(d => d.FechaCreacion, opt => opt.MapFrom(s => s.FECHACREACION))
            .ForMember(d => d.FechaModificacion, opt => opt.MapFrom(s => s.FECHAMODIFICACION));
        }
    }
}