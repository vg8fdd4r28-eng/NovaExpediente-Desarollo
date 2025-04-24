using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("especialidadConsultor", Schema = "NE")]
   public class EspecialidadConsultor : AuditableEntity
    {
        public EspecialidadConsultor()
        {

        }
        [Key]
        public int IDESPECIALIDADCONSULTOR { get; set; }
        public int IDESPECIALIDAD { get; set; }
         public int IDNIVELCONOCIMIENTO { get; set; }
       public int IDCOLABORADOR { get; set; }
        public int EXPERIENCIA { get; set; }

        [ForeignKey("IDESPECIALIDAD")]
        public virtual Especialidad Especialidad { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }
    }
}
