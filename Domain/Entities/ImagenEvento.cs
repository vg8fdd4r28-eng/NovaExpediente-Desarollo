using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("IMAGENEVENTO", Schema = "NE")]
    public class ImagenEvento : AuditableEntity
    {
        public ImagenEvento() { }
        [Key]
        public int IDIMAGENEVENTO { get; set; }
        public int IDIMAGEN { get; set; }
        public int IDEVENTO { get; set; }

        [ForeignKey("IDIMAGEN")]
        public virtual Imagen Imagen { get; set; }

        [ForeignKey("IDEVENTO")]
        public virtual Evento Evento { get; set; }
    }
}
