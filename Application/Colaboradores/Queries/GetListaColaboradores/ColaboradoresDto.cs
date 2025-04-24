using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Linq;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradores
{
    public class ColaboradoresDto : IMapFrom<Colaborador>
    {

        public int IDCOLABORADOR { get; set; }
        public int IDPUESTO { get; set; }
        public int IDPAISORIGEN { get; set; }
        public int IDPAISRESIDENCIA { get; set; }
        public int IDUSUARIOCREA { get; set; }
        public int? IDUSUARIOMODIFICA { get; set; }
        public int? IDCANTON { get; set; }
        public int? IDDELIVERYMANAGER { get; set; }
        public int? IDEMPRESACORPORACION { get; set; }
        public int? IDCLIENTE { get; set; }
        

        public string IDENTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string PRIMERAPELLIDO { get; set; }
        public string SEGUNDOAPELLIDO { get; set; }
        public DateTime FECHANACIMIENTO { get; set; }
        public char GENERO { get; set; }
        public DateTime FECHAINGRESO { get; set; }
        public string DIRECCION { get; set; }
        public string CELULAR { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO { get; set; }
        public string CORREOPERSONAL { get; set; }
        public string RESENAPERFIL { get; set; }
        public string NUMEROCPIC { get; set; }
        public string RAMAESPECIALIDAD { get; set; }
        public bool ACTIVO { get; set; }
        //  public image FOTO { get; set; }
        public char ESTADOCIVIL { get; set; }
        public string TELEFONOEMERGENCIA { get; set; }
        public string PERSONALLAMAR { get; set; }
        public int NUMEROHIJOS { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        //public timestamp CAMBIO { get; set; } //add nest?
        public DateTime INICIOVERSION
        { get; set; }
        public DateTime FINVERSION { get; set; }

        public string Puesto { get; set; }
        public string DeliveryManager { get; set; }

        public string NombreCompleto { get; set; }
        public string Estado { get; set; }

        public string Idiomas { get; set; }

        public string Certificaciones { get; set; }
        public bool ESDELIVERYMANAGER { get; set; }
        public string Cliente { get; set; }


        public void Mapping(Profile profile)
        {
            
 
            profile.CreateMap<Colaborador, ColaboradoresDto>()
                .ForMember(d => d.IDCOLABORADOR, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.IDDELIVERYMANAGER, opt => opt.MapFrom(s => s.IDDELIVERYMANAGER == null ? 0 : s.IDDELIVERYMANAGER))
                .ForMember(d => d.IDEMPRESACORPORACION, opt => opt.MapFrom(s => s.IDEMPRESACORPORACION == null ? 0 : s.IDEMPRESACORPORACION))
                .ForMember(d => d.IDCLIENTE, opt => opt.MapFrom(s => s.IDCLIENTE))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.ESDELIVERYMANAGER, opt => opt.MapFrom(s => s.ESDELIVERYMANAGER))
                .ForMember(d => d.DeliveryManager, opt => opt.MapFrom(s => s.DeliveryManager != null ? s.DeliveryManager.NOMBRE + " " + s.DeliveryManager.PRIMERAPELLIDO + " " + s.DeliveryManager.SEGUNDOAPELLIDO : "No Asignado"))
                .ForMember(d => d.NombreCompleto, opt => opt.MapFrom(s => s.NOMBRE.Trim() + " " + s.PRIMERAPELLIDO.Trim() + " " + s.SEGUNDOAPELLIDO.Trim()))
                .ForMember(d => d.RAMAESPECIALIDAD, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.RAMAESPECIALIDAD.Trim()) ? "Sin definir" : s.RAMAESPECIALIDAD))
                .ForMember(d => d.Estado, opt => opt.MapFrom(s => s.ACTIVO ? " Activo" : "Inactivo"))
                .ForMember(i => i.Idiomas, opt => opt.MapFrom(i => string.Join(", ", i.IdiomasColaborador.Select(a => a.Idioma.NOMBRE  + " - " + a.NivelIdioma.NOMBRE).ToArray())))
                .ForMember(i => i.Certificaciones, opt => opt.MapFrom(i => string.Join(", ", i.CertificacionesColaborador.Select(a => a.Certificacion.NOMBRE).ToArray())))
                .ForMember(d => d.Cliente, opt => opt.MapFrom(s => s.Cliente.NOMBRE))
                .ForMember(d => d.Puesto, opt => opt.MapFrom(s => s.Puesto.NOMBRE));
        }

       
    }
}

