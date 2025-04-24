using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{

    [Table("NIVELIDIOMA", Schema = "NE")]

    public class NivelIdioma : AuditableEntity
    {
        public NivelIdioma()
        {

        }
        [Key]
        public int IDNIVELIDIOMA { get; set; }
        public int IDIDIOMA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }

        public int ORDEN { get; set; }

        [ForeignKey("IDIDIOMA")]
        public virtual Idioma Idioma { get; set; }

    }
}
