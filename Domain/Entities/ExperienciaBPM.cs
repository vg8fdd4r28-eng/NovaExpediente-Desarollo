using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIABPM", Schema = "NE")]
    public class ExperienciaBPM : AuditableEntity
    {
        public ExperienciaBPM()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDBPM { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }


        [ForeignKey("IDBPM")]
        public virtual BPM BPM { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
