using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("INFRAESTRUCTURA", Schema = "NE")]
    public class Infraestructura : AuditableEntity
    {
        public Infraestructura()
        {

        }
        [Key]
        public int IDINFRAESTRUCTURA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }

    }
}
