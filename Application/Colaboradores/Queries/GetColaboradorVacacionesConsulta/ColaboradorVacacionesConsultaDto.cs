using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesConsulta
{
    public class ColaboradorVacacionesConsultaDto : IMapFrom<Colaborador>
    {


        public ColaboradorVacacionesConsultaDto()
        {
        }

        public int IDCOLABORADOR { get; set; }
        public string IDENTIFICACION { get; set; }

        public int? IDNOMINA { get; set; }

        public bool SOLICITAVACACIONES { get; set; }

        public int? VACACIONESDIASANUALES { get; set; }

        public decimal SALDOVACACIONES { get; set; }


        public decimal? DIASCORTE { get; set; }
        public int? VACACIONESDIASACUMULADO { get; set; }
        public decimal? VACACIONESADICIONALES { get; set; }
        public decimal? VacacionesDisponibles { get; set; }
        public decimal SALDOVACACIONESACTUAL { get; set; }
        public string NombreCompleto { get; set; }




        public void Mapping(Profile profile)
        {
            profile.CreateMap<Colaborador, ColaboradorVacacionesConsultaDto>()
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.IDENTIFICACION, opt => opt.MapFrom(s => s.IDENTIFICACION))
                .ForMember(d => d.NombreCompleto, opt => opt.MapFrom(s => s.NOMBRE + " " + s.PRIMERAPELLIDO + " " + s.SEGUNDOAPELLIDO))
                .ForMember(d => d.IDNOMINA, opt => opt.MapFrom(s => s.IDNOMINA))
                .ForMember(d => d.SOLICITAVACACIONES, opt => opt.MapFrom(s => s.SOLICITAVACACIONES))
                .ForMember(d => d.VACACIONESDIASANUALES, opt => opt.MapFrom(s => s.VACACIONESDIASANUALES))
                .ForMember(d => d.SALDOVACACIONES, opt => opt.MapFrom(s => s.SALDOVACACIONES))
                .ForMember(d => d.SALDOVACACIONESACTUAL, opt => opt.MapFrom(s => s.SALDOVACACIONESACTUAL))
                .ForMember(d => d.DIASCORTE, opt => opt.MapFrom(s => s.DIASCORTE))
                .ForMember(d => d.VACACIONESDIASACUMULADO, opt => opt.MapFrom(s => s.VACACIONESDIASACUMULADO))
                .ForMember(d => d.VACACIONESADICIONALES, opt => opt.MapFrom(s => s.VACACIONESADICIONALES))
                .ForMember(d => d.VacacionesDisponibles, opt => opt.MapFrom(s => (s.SALDOVACACIONES) + (s.VACACIONESDIASACUMULADO + s.VACACIONESADICIONALES - s.DIASCORTE)));

        }


    }
}
