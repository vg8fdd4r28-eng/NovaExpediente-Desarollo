using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("PROYECTOBASEDATOS", Schema = "NE")]
    public class ProyectoBaseDatos : AuditableEntity
    {
        public ProyectoBaseDatos()
        {

        }
        [Key]
        public int IDPROYECTOBASEDATOS { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDBASEDATOS { get; set; }
        

        [ForeignKey("IDPROYECTO")]
        public virtual Proyecto Proyecto { get; set; }

        [ForeignKey("IDBASEDATOS")]
        public virtual BaseDatos BaseDatos { get; set; }



    }
}
