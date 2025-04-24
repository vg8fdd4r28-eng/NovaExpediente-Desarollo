
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace NovaExpediente.Application.VacunasColaboradores.Queries.GetListaVacunasColaborador
{
    public class VacunasColaboradorPerfilDto : IMapFrom<VacunasColaborador>
    {

        [Key]
        public int IdVacunasColaborador { get; set; }

        public int IdVacuna { get; set; }

        public string NombreVacuna { get; set; }

        public DateTime Fecha { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VacunasColaborador, VacunasColaboradorPerfilDto>()
            .ForMember(d => d.IdVacunasColaborador, opt => opt.MapFrom(s => s.IdVacunasColaborador))
            .ForMember(d => d.IdVacuna, opt => opt.MapFrom(s => s.IdVacuna))
            .ForMember(d => d.Fecha, opt => opt.MapFrom(s => s.Fecha))
            .ForMember(d => d.NombreVacuna, opt => opt.MapFrom(s => s.Vacuna.Nombre));
        }
    }
}