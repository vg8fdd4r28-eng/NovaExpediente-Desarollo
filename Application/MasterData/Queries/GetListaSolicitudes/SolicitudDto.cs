using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Common;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetListaSolicitudes
{
    public enum RangoAntiguedad
    {
        Ninguno = 0,
        Verde = 1,
        Amarillo = 2,
        Rojo = 3
    }

    public class SolicitudDto : IMapFrom<SolicitudMasterData>
    {
        private readonly IDateTime _dateTime;
        public SolicitudDto()
        {
            _dateTime = new MachineDateTime();
        }
        public int IdSolicitud { get; set; }
        public int IdTipoSolicitud { get; set; }

        public string CodigoEstado { get; set; }
        public string Estado { get; set; }

        public string TipoSolicitud { get; set; }

        public int IdAutor { get; set; }
        public string Autor { get; set; }

        public int? IdColaborador { get; set; }
        public string NombreColaborador { get; set; }
        public int IdResponsable { get; set; }
        public string Responsable { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public int DiferenciaDias { get; set; }
        public RangoAntiguedad Rango { get; set; }
        public int IDUSUARIORESPONSABLE { get; set; }
        public string DeliveryManager { get; set; }
        public string Empresas { get; set; }
        public string EsNomina { get; set; }
        public string ColaboradorActivo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SolicitudMasterData, SolicitudDto>()

                           .ForMember(d => d.IdSolicitud, opt => opt.MapFrom(s => s.IDSOLICITUDMASTERDATA))
                           .ForMember(d => d.IdTipoSolicitud, opt => opt.MapFrom(s => s.IDTIPOSOLICITUD))
                           .ForMember(d => d.CodigoEstado, opt => opt.MapFrom(s => s.ESTADOSOLICITUD))
                           .ForMember(d => d.Estado, opt => opt.MapFrom(s => GetDescripcionEstado(s.ESTADOSOLICITUD)))
                           .ForMember(d => d.TipoSolicitud, opt => opt.MapFrom(s => s.TipoSolicitud.NOMBRE))
                           .ForMember(d => d.IdAutor, opt => opt.MapFrom(s => s.IDUSUARIOCREA))
                           .ForMember(d => d.Autor, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Autor.NOMBRE, s.Autor.PRIMERAPELLIDO, s.Autor.SEGUNDOAPELLIDO)))
                           .ForMember(d => d.IdColaborador, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                           .ForMember(d => d.NombreColaborador, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Colaborador.NOMBRE, s.Colaborador.PRIMERAPELLIDO, s.Colaborador.SEGUNDOAPELLIDO)))
                           .ForMember(d => d.IdResponsable, opt => opt.MapFrom(s => s.IDUSUARIOCREA))
                           .ForMember(d => d.IDUSUARIORESPONSABLE, opt => opt.MapFrom(s => s.IDUSUARIORESPONSABLE))
                           .ForMember(d => d.FechaCreacion, opt => opt.MapFrom(s => s.FECHACREACION))
                           .ForMember(d => d.FechaModificacion, opt => opt.MapFrom(s => s.FECHAMODIFICACION.HasValue ? s.FECHAMODIFICACION.Value : s.FECHACREACION))
                           .ForMember(d => d.DiferenciaDias, opt => opt.MapFrom(s => s.FECHAMODIFICACION.HasValue ? _dateTime.GetNumberOfWorkingDays(s.FECHAMODIFICACION.Value, DateTime.Now)
                                                                                                                  : _dateTime.GetNumberOfWorkingDays(s.FECHACREACION, DateTime.Now)))
                           //opt => opt.MapFrom(s => s.FECHAMODIFICACION.HasValue ? (DateTime.Now - s.FECHAMODIFICACION.Value).Days : (DateTime.Now - s.FECHACREACION).Days))
                           .ForMember(d => d.Rango, opt => opt.MapFrom(s => RangoAntiguedad.Ninguno))

                           .ForMember(d => d.DeliveryManager, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Colaborador.DeliveryManager.NOMBRE, s.Colaborador.DeliveryManager.PRIMERAPELLIDO, s.Colaborador.DeliveryManager.SEGUNDOAPELLIDO)))
                           .ForMember(d => d.Empresas, opt => opt.MapFrom(s => s.Colaborador.EmpresaCorporacion.NOMBRE))

                           .ForMember(d => d.EsNomina, opt => opt.MapFrom(s => s.Colaborador.IDNOMINA == null ? "Servicios profesionales / Otros casos" : s.Colaborador.IDNOMINA > 0 ? "Nomina" : "Servicios profesionales / Otros casos"))

                           .ForMember(d => d.ColaboradorActivo, opt => opt.MapFrom(s => s.Colaborador.ACTIVO == true ? "Si" : "No"))

                           .ForMember(d => d.Responsable, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Responsable.NOMBRE, s.Responsable.PRIMERAPELLIDO, s.Responsable.SEGUNDOAPELLIDO)));

        }
        public static string GetDescripcionEstado(string codigoEstado)
        {
            string descripcion = string.Empty;
            switch (codigoEstado)
            {
                case "RE":
                    descripcion = "Registrada";
                    break;
                case "AP":
                    descripcion = "Aprobada";
                    break;
                case "DE":
                    descripcion = "Denegada";
                    break;
                case "DC":
                    descripcion = "Descartada";
                    break;
                case "EA":
                    descripcion = "En aprobación";
                    break;
                case "CO":
                    descripcion = "En correcciones";
                    break;
                case "EV":
                    descripcion = "En validación";
                    break;
                case "PE":
                    descripcion = "Pendiente de actualización";
                    break;
                case "EP":
                    descripcion = "En proceso";
                    break;
                case "EPV":
                    descripcion = "En proceso";
                    break;
                case "AN":
                    descripcion = "Anulada";
                    break;
            }
            return descripcion;
        }
    }
}
