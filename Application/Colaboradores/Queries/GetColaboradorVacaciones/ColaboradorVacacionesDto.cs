using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacaciones
{
    public class ColaboradorVacacionesDto : IMapFrom<Colaborador>
    {


        public ColaboradorVacacionesDto()
        {
        }

        public decimal SALDOVACACIONESACTUAL { get; set; }
        public int IDNOMINA { get; set; }
        public bool ESSERVICIOPROFESIONAL { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<Colaborador, ColaboradorVacacionesDto>()
                .ForMember(d => d.IDNOMINA, opt => opt.MapFrom(s => s.IDNOMINA))
                .ForMember(d => d.ESSERVICIOPROFESIONAL, opt => opt.MapFrom(s => s.ESSERVICIOPROFESIONAL))
                .ForMember(d => d.SALDOVACACIONESACTUAL, opt => opt.MapFrom(s => s.SALDOVACACIONESACTUAL));

        }


    }
}
