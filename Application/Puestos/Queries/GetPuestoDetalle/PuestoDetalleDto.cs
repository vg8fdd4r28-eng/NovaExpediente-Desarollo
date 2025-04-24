using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Puestos.Queries.GetPuestoDetalle
{
    public class PuestoDetalleDto : IMapFrom<Puesto>
    {
        public int IdPuesto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsDm { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Puesto, PuestoDetalleDto>()
                .ForMember(d => d.IdPuesto, opt => opt.MapFrom(s => s.IDPUESTO))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE));
              
        }


    }
}
