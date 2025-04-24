using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("FERIADO", Schema = "NE")]
    public class Feriado : AuditableEntity
    {
        public Feriado()
        {
        }
        [Key]
        public int IDFERIADO { get; set; }
        public int IDPAIS { get; set; }
        public DateTime FECHA { get; set; }
        public string DESCRIPCION { get; set; }

        [ForeignKey("IDPAIS")]
        public virtual Pais Pais { get; set; }
    }
}
