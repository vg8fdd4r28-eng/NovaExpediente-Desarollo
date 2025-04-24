using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIACALIDAD", Schema = "NE")]
    public class ExperienciaCalidad : AuditableEntity
    {
        public ExperienciaCalidad()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDCALIDAD { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }


        [ForeignKey("IDCALIDAD")]
        public virtual Calidad Calidad { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
