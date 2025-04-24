
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Application.VacunasColaboradores.Queries.GetListaVacunasColaborador;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorPerfil
{
    public class VacunacionColaboradorPerfilDto : IMapFrom<VacunacionColaborador>
    {
        public int IdColaborador { get; set; }
        public int IdVacunacionColaborador { get; set; }
        public bool TieneCertificado { get; set; }

        public List<VacunasColaboradorPerfilDto> vacunasColaboradorPerfil { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<VacunacionColaborador, VacunacionColaboradorPerfilDto>()

                .ForMember(d => d.IdColaborador, opt => opt.MapFrom(s => s.IdColaborador))
                .ForMember(d => d.IdVacunacionColaborador, opt => opt.MapFrom(s => s.IdVacunacionColaborador))
                .ForMember(d => d.TieneCertificado, opt => opt.MapFrom(s => s.Certificado != null))
                .ForMember(d => d.vacunasColaboradorPerfil, opt => opt.MapFrom(s => s.VacunasColaborador));



        }
    }
}
