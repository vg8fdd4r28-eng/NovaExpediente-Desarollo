using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.MasterData.Queries.GetResponsablePorPerfil
{
    public class ResponsablePorPerfilDto : IMapFrom<Colaborador>
    {

        public string IDENTIFICACION { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBRE { get; set; }
        public string PRIMERAPELLIDO { get; set; }
        public string SEGUNDOAPELLIDO { get; set; }
        public string CORREO { get; set; }
        public string NombreCompleto { get; set; }
        public int? IDDELIVERYMANAGER { get; set; }



        public void Mapping(Profile profile)
        {

            profile.CreateMap<Colaborador, ResponsablePorPerfilDto>()
            .ForMember(d => d.IDENTIFICACION, opt => opt.MapFrom(s => s.IDENTIFICACION))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.IDDELIVERYMANAGER, opt => opt.MapFrom(s => s.IDDELIVERYMANAGER))
                .ForMember(d => d.PRIMERAPELLIDO, opt => opt.MapFrom(s => s.PRIMERAPELLIDO))
                .ForMember(d => d.SEGUNDOAPELLIDO, opt => opt.MapFrom(s => s.SEGUNDOAPELLIDO))
                .ForMember(d => d.CORREO, opt => opt.MapFrom(s => s.CORREO))
                .ForMember(d => d.NombreCompleto, opt => opt.MapFrom(s => s.NOMBRE + " " + s.PRIMERAPELLIDO + " " + s.SEGUNDOAPELLIDO))
;
        }


    }
}
