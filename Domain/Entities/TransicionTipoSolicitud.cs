using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Domain.Entities
{
    [Table("TRANSICIONTIPOSOLICITUD", Schema = "NE")]
    public class TransicionTipoSolicitud : AuditableEntity
    {
        public TransicionTipoSolicitud()
        {

        }
        [Key]
        public int IDTRANSICIONTIPOSOLICITUD { get; set; }
        public int IDTIPOSOLICITUD { get; set; }

        public string ESTADOACTUAL { get; set; }
        public string ACCION { get; set; }
        public string ESTADOSIGUIENTE { get; set; }
        public string PERFILRESPONSABLE { get; set; }
        public int? IDRESPONSABLEESPECIFICO { get; set; }
        public string CORREO_CC_NOTIFICAR { get; set; }

        [ForeignKey("IDTIPOSOLICITUD")]
        public virtual TipoSolicitud TipoSolicitud { get; set; }
        [ForeignKey("IDRESPONSABLEESPECIFICO")]
        public virtual Colaborador ResponsableEspecifico{ get; set; }
    }
}
