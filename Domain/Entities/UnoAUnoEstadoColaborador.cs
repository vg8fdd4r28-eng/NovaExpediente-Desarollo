using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("UNOAUNOESTADOCOLABORADOR", Schema = "NE")]
    public class UnoAUnoEstadoColaborador : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDUNOAUNOESTADOCOLABORADOR { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string NOMBRE { get; set; }

        [Required]
        public bool ACTIVO { get; set; } = true;
        
    }
}
