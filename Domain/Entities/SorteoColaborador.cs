using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("SORTEOCOLABORADOR", Schema = "NE")]
    public class SorteoColaborador : AuditableEntity
    {
        public SorteoColaborador() { }
        [Key]
        public int IDSORTEOCOLABORADOR { get; set; }
        public int IDSORTEO { get; set; }
        public int IDCOLABORADOR { get; set; }

        [ForeignKey("IDSORTEO")]
        public virtual Sorteo Sorteo { get; set; }
        [ForeignKey("IDCOLABORADOR")]
        public virtual Colaborador Colaborador { get; set; }
        [ForeignKey("IDUSUARIOCREA")]
        public virtual Colaborador ColaboradorCrea { get; set; }
        [ForeignKey("IDUSUARIOMODIFICA")]
        public virtual Colaborador ColaboradorModifica { get; set; }
    }
}
