using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("HERRAMIENTAPROYECTO", Schema = "NE")]
    public class HerramientaProyecto : AuditableEntity
    {
        public HerramientaProyecto()
        {

        }
        [Key]
        public int IDHERRAMIENTAPROYECTO { get; set; }
        public int IDEXPERIENCIAPROYECTO { get; set; }
        public string NOMBRE { get; set; }



 

    }
}
