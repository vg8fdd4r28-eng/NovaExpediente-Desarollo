using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Colaboradores.Queries.GetInfoColaboradorEmail
{
    public class InfoColaboradorDto : IMapFrom<Colaborador>
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int ContadorInbox { get; set; }
        public int ContadorAdminInbox { get; set; }
        public int? IDNOMINA { get; set; }
        public int IdPaisResidencia { get; set; }
        public bool SolicitaVacaciones{ get; set; }
        public decimal SaldoVacacionesActual { get; set; }
        public bool Activo { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Colaborador, InfoColaboradorDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.Identificacion, opt => opt.MapFrom(s => s.IDENTIFICACION))
                .ForMember(d => d.IDNOMINA, opt => opt.MapFrom(s => s.IDNOMINA))
                .ForMember(d => d.IdPaisResidencia, opt => opt.MapFrom(s => s.IDPAISRESIDENCIA))
                .ForMember(d => d.SolicitaVacaciones, opt => opt.MapFrom(s => s.SOLICITAVACACIONES))
                .ForMember(d => d.SaldoVacacionesActual, opt => opt.MapFrom(s => s.SALDOVACACIONESACTUAL))
                .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.NOMBRE, s.PRIMERAPELLIDO, s.SEGUNDOAPELLIDO)))
                .ForMember(d => d.Correo, opt => opt.MapFrom(s => s.CORREO));
        }


    }
}
