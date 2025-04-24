using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.Clasificadores.Queries.GetListaClasificadores
{
   public class ClasificadoresDto : IMapFrom<Clasificador>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Clasificador, ClasificadoresDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IdClasificador))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombre));
        }
    }
}
