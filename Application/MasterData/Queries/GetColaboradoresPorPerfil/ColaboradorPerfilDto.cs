using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetColaboradoresPorPerfil
{
    public class ColaboradorPerfilDto : IMapFrom<Colaborador>
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


            profile.CreateMap<Colaborador, ColaboradorPerfilDto>()
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.NombreCompleto, opt => opt.MapFrom(s => s.NOMBRE.Trim() + " " + s.PRIMERAPELLIDO.Trim() + " " + s.SEGUNDOAPELLIDO.Trim()))
;
        }


    }
}
