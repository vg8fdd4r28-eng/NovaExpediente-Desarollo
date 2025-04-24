using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("PROYECTOCLASIFICADOR", Schema = "NE")]
    public class ProyectoClasificador : AuditableEntity
    {
        public ProyectoClasificador()
        {

        }
        [Key]
        public int IDPROYECTO { get; set; }
        public int IDCLASIFICADOR { get; set; }


        [ForeignKey("IDPROYECTO")]
        public virtual Proyecto Proyecto { get; set; }

        [ForeignKey("IDCLASIFICADOR")]
        public virtual Clasificador Clasificador { get; set; }



    }
}
