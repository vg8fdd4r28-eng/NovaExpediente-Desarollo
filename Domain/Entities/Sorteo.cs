using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("SORTEO", Schema = "NE")]
    public class Sorteo : AuditableEntity
    {
        public Sorteo() { }
        [Key]
        public int IDSORTEO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ACTIVO { get; set; }
        public DateTime FECHAVENCIMIENTO { get; set; }
        [ForeignKey("IDUSUARIOCREA")]
        public virtual Colaborador ColaboradorCrea { get; set; }
        [ForeignKey("IDUSUARIOMODIFICA")]
        public virtual Colaborador ColaboradorModifica { get; set; } 
    }
}
