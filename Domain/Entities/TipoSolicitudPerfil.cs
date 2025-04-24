using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("TIPOSOLICITUDPERFIL", Schema = "NE")]
    public class TipoSolicitudPerfil : AuditableEntity
    {
        public  TipoSolicitudPerfil()
        {

        }
        [Key]
        public int IDTIPOSOLICITUDPERFIL { get; set; }
        public int IDTIPOSOLICITUD { get; set; }
        public string NOMBRE_DEL_PERFIL { get; set; }
        public string ESTADOSOLICITUD { get; set; }

        [ForeignKey("IDTIPOSOLICITUD")]
        public virtual TipoSolicitud TipoSolicitud { get; set; }
    }
}
