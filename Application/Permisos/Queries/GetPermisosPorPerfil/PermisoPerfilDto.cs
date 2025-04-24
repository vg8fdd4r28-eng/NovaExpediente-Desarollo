using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.Permisos.Queries.GetPermisosPorPerfil
{
    public class PermisoPerfilDto : IMapFrom<Permiso>
    {

        public int IDPERMISOTRABAJO { get; set; }
        public int IDTIPOPERMISOGOCE { get; set; }
        public int IDCOLABORADOR { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public int CANTIDADDIAS { get; set; }
        public string DESCRIPCION { get; set; }
        public string TIPOPERMISO { get; set; }


        public string colaboradorNombre { get; set; }
        public string identificacionColaborador { get; set; }
        public int IdSolicitud { get; set; }
        public string TIPOPERMISOGOCE { get; set; }
        public string Estado { get; set; }


        public void Mapping(Profile profile)
        {


            profile.CreateMap<Permiso, PermisoPerfilDto>()
                .ForMember(d => d.IDPERMISOTRABAJO, opt => opt.MapFrom(s => s.IDPERMISOTRABAJO))
                .ForMember(d => d.IDTIPOPERMISOGOCE, opt => opt.MapFrom(s => s.IDTIPOPERMISOGOCE))
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.FECHAINICIO, opt => opt.MapFrom(s => s.FECHAINICIO))
                .ForMember(d => d.FECHAFINAL, opt => opt.MapFrom(s => s.FECHAFINAL))
                .ForMember(d => d.CANTIDADDIAS, opt => opt.MapFrom(s => s.CANTIDADDIAS))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION))
                .ForMember(d => d.TIPOPERMISO, opt => opt.MapFrom(s => s.TIPOPERMISO))
                .ForMember(d => d.TIPOPERMISOGOCE, opt => opt.MapFrom(s => s.TIPOPERMISO == "G" ? s.TipoPermisoGoce.NOMBRE : "No aplica"))
                .ForMember(d => d.IdSolicitud, opt => opt.MapFrom(s => s.IDSOLICITUDMASTERDATA))
                .ForMember(d => d.identificacionColaborador, opt => opt.MapFrom(s => s.Colaborador.IDENTIFICACION))
                .ForMember(d => d.Estado, opt => opt.MapFrom(s => s.SolicitudMasterData.ESTADOSOLICITUD))
                .ForMember(d => d.colaboradorNombre, opt => opt.MapFrom(s => s.Colaborador.NOMBRE + " " + s.Colaborador.PRIMERAPELLIDO + " " + s.Colaborador.SEGUNDOAPELLIDO))
;
        }


    }
}
