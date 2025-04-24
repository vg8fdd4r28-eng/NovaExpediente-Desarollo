using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("PROYECTOLENGUAJE", Schema = "NE")]
    public class ProyectoLenguaje : AuditableEntity
    {
        public ProyectoLenguaje()
        {

        }
        [Key]
        public int IDPROYECTOLENGUAJE { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDLENGUAJEPROGRAMACION { get; set; }
        

        [ForeignKey("IDPROYECTO")]
        public virtual Proyecto Proyecto { get; set; }

        [ForeignKey("IDLENGUAJEPROGRAMACION")]
        public virtual LenguajeProgramacion LenguajeProgramacion { get; set; }



    }
}
