using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EVENTOSECCION", Schema = "NE")]
    public class EventoSeccion : AuditableEntity
    {
        [Key]
        public int IDEVENTOSECCION { get; set; }

        [Required, StringLength(50)]

        public string NOMBRE { get; set; }

        [Required, StringLength(100)]
        public string TITULO { get; set; }

        [Required, StringLength(50)]
        public string TIPO { get; set; }

        [Required]
        public string OPCIONES { get; set; }

        [Required]
        public int IDEVENTO { get; set; }

        [Required]
        public bool ESREQUERIDO { get; set; }

        [ForeignKey("IDEVENTO")]
        public virtual Evento Evento { get; set; }

    }
}
