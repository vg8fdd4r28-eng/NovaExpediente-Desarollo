using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIAERP", Schema = "NE")]
    public class ExperienciaERP : AuditableEntity
    {
        public ExperienciaERP()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDERPCORE { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }
        

        [ForeignKey("IDERPCORE")]
        public virtual ErpCore ErpCore { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
