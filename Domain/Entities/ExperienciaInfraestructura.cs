using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIAINFRAESTRUCTURA", Schema = "NE")]
  public  class ExperienciaInfraestructura : AuditableEntity
    {
        public ExperienciaInfraestructura()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDINFRAESTRUCTURA { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }


        [ForeignKey("IDINFRAESTRUCTURA")]
        public virtual Infraestructura Infraestructura { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
