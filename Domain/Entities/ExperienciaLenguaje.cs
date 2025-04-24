using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIALENGUAJE", Schema = "NE")]
    public class ExperienciaLenguaje : AuditableEntity
    {
        public ExperienciaLenguaje()
        {

        }
        [Key]
        public int IDLENGUAJEPROGRAMACION { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }


        [ForeignKey("IDLENGUAJEPROGRAMACION")]
        public virtual LenguajeProgramacion LenguajeProgramacion { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
