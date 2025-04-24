using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIATECNOLOGIA", Schema = "NE")]
    public class ExperienciaTecnologia : AuditableEntity
    {
        public ExperienciaTecnologia()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDTECNOLOGIA { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }


        [ForeignKey("IDTECNOLOGIA")]
        public virtual Tecnologia Tecnologia { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
