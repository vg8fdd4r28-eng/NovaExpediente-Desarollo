using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.CentrosEstudios.Queries.GetCentroEstudioDetalle
{
    public class CentroEstudioDetalleDto : IMapFrom<CentroEstudio>
    {

        public int IdCentroEstudio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<CentroEstudio, CentroEstudioDetalleDto>()
                .ForMember(d => d.IdCentroEstudio, opt => opt.MapFrom(s => s.IDCENTROESTUDIO))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.Descripcion));
        }
    }
}
