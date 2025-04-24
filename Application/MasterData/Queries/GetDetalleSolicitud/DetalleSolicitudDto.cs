using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Application.MasterData.Queries.GetListaSolicitudes;
using NovaExpediente.Common;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetDetalleSolicitud
{
    public class DetalleSolicitudDto : SolicitudDto, IMapFrom<SolicitudMasterData>
    {
        //public int IdSolicitudMasterData { get; set; }
        //public int IdTipoSolicitud { get; set; }
        //public string TipoSolicitud { get; set; }
        //public string EstadoSolicitud { get; set; }

        public string DocSolicitud { get; set; }
        public List<ComentarioSolicitudDto> Comentarios { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SolicitudMasterData, DetalleSolicitudDto>()
            .ForMember(d => d.IdSolicitud, opt => opt.MapFrom(s => s.IDSOLICITUDMASTERDATA))
            .ForMember(d => d.IdTipoSolicitud, opt => opt.MapFrom(s => s.IDTIPOSOLICITUD))
            .ForMember(d => d.CodigoEstado, opt => opt.MapFrom(s => s.ESTADOSOLICITUD))
            .ForMember(d => d.Estado, opt => opt.MapFrom(s => GetDescripcionEstado(s.ESTADOSOLICITUD)))
            .ForMember(d => d.TipoSolicitud, opt => opt.MapFrom(s => s.TipoSolicitud.NOMBRE))
            .ForMember(d => d.DocSolicitud, opt => opt.MapFrom(s => s.DOCSOLICITUD))
            .ForMember(d => d.IdAutor, opt => opt.MapFrom(s => s.IDUSUARIOCREA))
            .ForMember(d => d.Autor, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Autor.NOMBRE, s.Autor.PRIMERAPELLIDO, s.Autor.SEGUNDOAPELLIDO)))
            .ForMember(d => d.IdColaborador, opt => opt.MapFrom(s => s.IDCOLABORADOR))
            .ForMember(d => d.NombreColaborador, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Colaborador.NOMBRE, s.Colaborador.PRIMERAPELLIDO, s.Colaborador.SEGUNDOAPELLIDO)))
            .ForMember(d => d.IdResponsable, opt => opt.MapFrom(s => s.IDUSUARIOCREA))
            .ForMember(d => d.Responsable, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Responsable.NOMBRE, s.Responsable.PRIMERAPELLIDO, s.Responsable.SEGUNDOAPELLIDO)))
            .ForMember(d => d.Comentarios, opt => opt.MapFrom(s => s.Comentarios));

        }
    }
}
