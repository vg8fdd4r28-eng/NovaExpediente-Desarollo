using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("UNOAUNOCATEGORIA", Schema = "NE")]
    public class UnoAUnoCategoria : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDUNOAUNOCATEGORIA { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string NOMBRE { get; set; }

        [Required]
        public bool ACTIVO { get; set; } = true;

    }
}
