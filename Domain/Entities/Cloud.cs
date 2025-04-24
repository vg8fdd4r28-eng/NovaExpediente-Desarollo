using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CLOUD", Schema = "NE")]
    public class Cloud : AuditableEntity
    {
        public Cloud()
        {

        }
        [Key]
        public int IDCLOUD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string VERSIONES { get; set; }

    }
}
