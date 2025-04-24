using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetDetalleSolicitud
{
    public class ComentarioSolicitudDto : IMapFrom<SolicitudComentario>
    {
        public int IdComentarioSolicitud { get; set; }
        public string Responsable { get; set; }
        public string Comentario { get; set; }
        public string Accion { get; set; }
        public string Fecha { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SolicitudComentario, ComentarioSolicitudDto>()
            .ForMember(d => d.IdComentarioSolicitud, opt => opt.MapFrom(s => s.IDSOLICITUDCOMENTARIO))
            .ForMember(d => d.Responsable, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Responsable.NOMBRE, s.Responsable.PRIMERAPELLIDO, s.Responsable.SEGUNDOAPELLIDO)))
            .ForMember(d => d.Comentario, opt => opt.MapFrom(s => s.COMENTARIO))
            .ForMember(d => d.Accion, opt => opt.MapFrom(s => s.ACCION))
            .ForMember(d => d.Fecha, opt => opt.MapFrom(s => s.FECHACREACION.ToString("dd/MM/yyyy HH:mm")));
        }


    }
}
