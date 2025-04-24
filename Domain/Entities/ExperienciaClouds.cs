using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIACLOUD", Schema = "NE")]
    public class ExperienciaCloud : AuditableEntity
    {
        public ExperienciaCloud()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDCLOUD { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }


        [ForeignKey("IDCLOUD")]
        public virtual Cloud Cloud { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
