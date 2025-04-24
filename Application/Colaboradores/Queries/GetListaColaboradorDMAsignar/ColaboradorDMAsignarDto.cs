using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDMAsignar
{
    public class ColaboradorDMAsignarDto : IMapFrom<Colaborador>
    {

        public int IDCOLABORADOR { get; set; }
        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }

        public string IDENTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string PRIMERAPELLIDO { get; set; }
        public string SEGUNDOAPELLIDO { get; set; }
        public int? IDDELIVERYMANAGER { get; set; }

        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }

        public string NombreCompleto { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Colaborador, ColaboradorDMAsignarDto>()
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.IDENTIFICACION, opt => opt.MapFrom(s => s.IDENTIFICACION))
                .ForMember(d => d.IDDELIVERYMANAGER, opt => opt.MapFrom(s => s.IDDELIVERYMANAGER == null ? 0 : s.IDDELIVERYMANAGER))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.PRIMERAPELLIDO, opt => opt.MapFrom(s => s.PRIMERAPELLIDO))
                .ForMember(d => d.SEGUNDOAPELLIDO, opt => opt.MapFrom(s => s.SEGUNDOAPELLIDO))
                .ForMember(d => d.NombreCompleto, opt => opt.MapFrom(s => s.NOMBRE + " " + s.PRIMERAPELLIDO + " " + s.SEGUNDOAPELLIDO));


        }


    }
}
