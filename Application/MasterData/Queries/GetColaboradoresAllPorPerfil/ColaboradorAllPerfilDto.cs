using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.MasterData.Queries.GetColaboradoresAllPorPerfil
{
    public class ColaboradorAllPerfilDto : IMapFrom<Colaborador>
    {

        public int IDCOLABORADOR { get; set; }
        public string IDENTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string PRIMERAPELLIDO { get; set; }
        public string SEGUNDOAPELLIDO { get; set; }
        public DateTime FECHANACIMIENTO { get; set; }
        public string NombreCompleto { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<Colaborador, ColaboradorAllPerfilDto>()
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.NombreCompleto, opt => opt.MapFrom(s => s.NOMBRE.Trim() + " " + s.PRIMERAPELLIDO.Trim() + " " + s.SEGUNDOAPELLIDO.Trim()))
;
        }


    }
}
