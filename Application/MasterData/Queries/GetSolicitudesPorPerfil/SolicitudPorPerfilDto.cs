using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetSolicitudesPorPerfil
{
    public class SolicitudPorPerfilDto : IMapFrom<TipoSolicitudPerfil>
    {
        public int IdTipoSolicitud { get; set; }
        public string TipoSolicitud { get; set; }
        
        public void Mapping(Profile profile)
        {


            profile.CreateMap<TipoSolicitudPerfil, SolicitudPorPerfilDto>()

                .ForMember(d => d.IdTipoSolicitud, opt => opt.MapFrom(s => s.IDTIPOSOLICITUD))
                .ForMember(d => d.TipoSolicitud, opt => opt.MapFrom(s => s.TipoSolicitud.NOMBRE));

        }
    }
}
