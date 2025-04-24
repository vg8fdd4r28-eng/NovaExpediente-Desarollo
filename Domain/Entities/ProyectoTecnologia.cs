using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("PROYECTOTECNOLOGIA", Schema = "NE")]
    public class ProyectoTecnologia : AuditableEntity
    {
        public ProyectoTecnologia()
        {

        }
        [Key]
        public int IDPROYECTOTECNOLOGIA { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDTECNOLOGIA { get; set; }
        

        [ForeignKey("IDPROYECTO")]
        public virtual Proyecto Proyecto { get; set; }

        [ForeignKey("IDTECNOLOGIA")]
        public virtual Tecnologia Tecnologia { get; set; }



    }
}
