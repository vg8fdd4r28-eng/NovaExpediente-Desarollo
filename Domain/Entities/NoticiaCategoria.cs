using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("NOTICIACATEGORIA", Schema = "NE")]
    public class NoticiaCategoria : AuditableEntity
    {
        public NoticiaCategoria() { }

        [Key]
        public int IDNOTICIACATEGORIA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ACTIVO { get; set; }
    }
}
