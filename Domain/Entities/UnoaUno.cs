using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("UNOAUNO", Schema = "NE")]
    public class UnoAUno : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDUNOAUNO { get; set; }

        [Required]
        public int IDCOLABORADOR { get; set; }

        [Required]
        public int IDUNOAUNOESTADOCOLABORADOR { get; set; }

        [Required]
        public DateTime FECHA_EJECUCION { get; set; } = DateTime.Now;

        public string OBSERVACION { get; set; }

        [Required]
        public bool ACTIVO { get; set; } = true;

        [ForeignKey("IDCOLABORADOR")]
        public virtual Colaborador Colaborador { get; set; }

        [ForeignKey("IDUNOAUNOESTADOCOLABORADOR")]
        public virtual UnoAUnoEstadoColaborador UnoAUnoEstadoColaborador { get; set; }

    }
}
