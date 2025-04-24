using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("TIPOSOLICITUD", Schema = "NE")]
    public class TipoSolicitud : AuditableEntity
    {
        public TipoSolicitud()
        {

        }
        [Key]
        public int IDTIPOSOLICITUD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBREPROCEDIMIENTO { get; set; }
        public string CATEGORIA { get; set; }

    }
}
