using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Idiomas.Queries.GetIdiomasDetalle
{
    public class IdiomaDetalleDto : IMapFrom<Idioma>
    {
        public int IdIdioma { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {


            profile.CreateMap<Idioma, IdiomaDetalleDto>()

                .ForMember(d => d.IdIdioma, opt => opt.MapFrom(s => s.IDIDIOMA))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(s => s.DESCRIPCION));

        }
    }
}
