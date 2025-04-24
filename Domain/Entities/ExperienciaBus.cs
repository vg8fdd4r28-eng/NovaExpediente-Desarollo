using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIABUS", Schema = "NE")]
    public  class ExperienciaBus : AuditableEntity
    {
        public ExperienciaBus()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDBUS { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }


        [ForeignKey("IDBUS")]
        public virtual Bus Bus { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
