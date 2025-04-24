using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NovaExpediente.Application.AplicacionMovil.Eventos.GetListaEventos
{
    public class ImagenDto : IMapFrom<Imagen>
    {
        public int IDIMAGEN { get; set; }

        [Required, StringLength(200)]
        public string URLIMAGEN { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Imagen, ImagenDto >()
                .ForMember(d => d.IDIMAGEN, opt => opt.MapFrom(s => s.IDIMAGEN))
                .ForMember(d => d.URLIMAGEN, opt => opt.MapFrom(s => s.URLIMAGEN));
        }
    }
}
